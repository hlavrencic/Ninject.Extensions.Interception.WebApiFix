using Ninject.Activation;
using Ninject.Syntax;
using Ninject.Web.Common;
using System.Linq;

namespace Investigacion.Web.Test.App_Start
{
    public static class ScopeHelper
    {
        public static IBindingNamedWithOrOnSyntax<T> InRequestScope<T>(this IBindingInSyntax<T> binding)
        {
            return binding.InScope(
                context =>
                    context.GetHttpRequestScope()); 
        }      

        public static object GetHttpRequestScope(this IContext context)
        {
            if(context == null)
            {
                return null;
            }

            var scope = context.Kernel.Components
                .GetAll<INinjectHttpApplicationPlugin>()
                .Select(c => c.GetRequestScope(context))
                .FirstOrDefault(s => s != null);
            return scope;
        }
    }
}
