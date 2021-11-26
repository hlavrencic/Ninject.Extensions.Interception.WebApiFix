using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Planning.Strategies;
using System;
using System.Linq;

namespace Ninject.Extensions.Interception.ProxyAttributes
{
    public static class NinjectComponentExtensions
    {
        public static void AddInterceptor<TAttribute, TInterceptor>(this IHaveKernel module)
            where TAttribute : Attribute
            where TInterceptor : IInterceptor
        {
            module.Kernel.AddInterceptor<TAttribute, TInterceptor>();
        }

        public static void AddInterceptor<TAttribute, TInterceptor>(this IKernel kernel)
            where TAttribute : Attribute
            where TInterceptor : IInterceptor
        {
            kernel.Components.AddComponent<TAttribute, TInterceptor>();
        }

        private static void AddComponent<TAttribute, TInterceptor>(this IComponentContainer componentContainer)
            where TAttribute : Attribute
            where TInterceptor : IInterceptor
        {
            componentContainer.Add<IPlanningStrategy, CustomPlanningStrategy<TAttribute, TInterceptor>>();

            var kernel = componentContainer.Kernel;
            if (kernel.GetBindings(typeof(InterceptorImplementation<>)).Any())
            {
                return;
            }

            kernel.Bind(typeof(InterceptorImplementation<>)).ToSelf().InTransientScope();
        }
    }
}
