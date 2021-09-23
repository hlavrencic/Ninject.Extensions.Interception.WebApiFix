using Ninject;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Request;
using System;

namespace Investigacion.Web.Test.App_Start
{
    public sealed class TransactionAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            // request.Kernel.GetHashCode()  !=  del kernel utilizado en la WebApi ==> kernel.BeginBlock().GetHashCode()
            return request.Kernel.Get<TransactionInterceptor>();
        }
    }

    public sealed class TransactionInterceptorAttribute : Attribute
    {
    }
}