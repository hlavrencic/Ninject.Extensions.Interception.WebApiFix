using System.Linq;
using Ninject.Components;
using Ninject.Planning.Strategies;
using System;
using Ninject.Extensions.Interception.Advice;
using Ninject.Extensions.Interception.Registry;
using Ninject.Planning;
using System.Collections.Generic;
using System.Reflection;
using Ninject.Extensions.Interception.Planning.Directives;

namespace Ninject.Extensions.Interception.ProxyAttributes
{
    public class CustomPlanningStrategy<TAttribute, TInterceptor> :
    NinjectComponent, IPlanningStrategy
        where TAttribute : Attribute
        where TInterceptor : IInterceptor
    {
        private readonly IAdviceFactory adviceFactory;
        private readonly IAdviceRegistry adviceRegistry;

        public CustomPlanningStrategy(
            IAdviceFactory adviceFactory, IAdviceRegistry adviceRegistry)
        {
            this.adviceFactory = adviceFactory;
            this.adviceRegistry = adviceRegistry;
        }

        public void Execute(IPlan plan)
        {
            var methods = GetCandidateMethods(plan.Type);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(
                    typeof(TAttribute), true) as TAttribute[];
                if (attributes.Length == 0)
                {
                    continue;
                }

                var advice = adviceFactory.Create(method);

                advice.Callback = request => request.Kernel.Get<TInterceptor>();
                adviceRegistry.Register(advice);

                if (!plan.Has<ProxyDirective>())
                {
                    plan.Add(new ProxyDirective());
                }
            }
        }

        private static IEnumerable<MethodInfo> GetCandidateMethods(Type type)
        {
            var methods = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            return methods.Where(ShouldIntercept);
        }

        private static bool ShouldIntercept(MethodInfo methodInfo)
        {
            return methodInfo.DeclaringType != typeof(object) &&
                   !methodInfo.IsPrivate &&
                   !methodInfo.IsFinal;
        }
    }
}
