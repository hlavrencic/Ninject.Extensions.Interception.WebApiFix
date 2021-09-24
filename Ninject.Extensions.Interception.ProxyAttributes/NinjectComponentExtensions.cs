using Ninject.Components;
using Ninject.Planning.Strategies;
using System;

namespace Ninject.Extensions.Interception.ProxyAttributes
{
    public static class NinjectComponentExtensions
    {
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
        }
    }
}
