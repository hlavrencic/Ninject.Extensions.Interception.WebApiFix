using Ninject;
using Ninject.Web.WebApi;
using System.Web.Http.Dependencies;

namespace Investigacion.Web.Test.App_Start
{
    public class MyNinjectDependencyResolver : MyNinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel kernel;

        public MyNinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new MyNinjectDependencyScope(this.kernel.BeginBlock());
        }
    }
}