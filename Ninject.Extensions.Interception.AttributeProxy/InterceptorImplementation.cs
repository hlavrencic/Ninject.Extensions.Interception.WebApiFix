using Ninject.Syntax;

namespace Ninject.Extensions.Interception.AttributeProxy
{
    public class InterceptorImplementation<TInterceptor> : IInterceptor
        where TInterceptor : IInterceptor
    {
        private readonly IResolutionRoot provider;

        public InterceptorImplementation(IResolutionRoot provider)
        {
            this.provider = provider;
        }

        public void Intercept(IInvocation invocation)
        {
            var interceptor = provider.Get<TInterceptor>();
            interceptor.Intercept(invocation);
        }
    }
}
