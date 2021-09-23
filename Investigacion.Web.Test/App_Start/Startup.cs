using System.Web.Http;
using Ninject;
using Owin;
using System.Web.Http.Filters;
using System.Web.Http.Validation;
using Investigacion.Web.Test.Controllers;
using Investigacion.Web.Test.Services;
using Ninject.Web.WebApi;
using Ninject.Extensions.Interception.Infrastructure.Language;
using System.Linq;

namespace Investigacion.Web.Test.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var kernel = CreateKernel();
            var config = new HttpConfiguration();
            //config.DependencyResolver = new NinjectDependencyResolver(kernel);

            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            app.UseNinject(CreateKernel).UseNinjectWebApi(config);
            app.UseWebApi(config);
            app.UseErrorPage();
            app.UseWelcomePage("/");
        }

        private StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
    
            kernel
                .Bind<IServiceExample>()
                .To<ServiceExample>()
                .Intercept(m => // Aplico el interceptor solo a aquellos metodos con atributo TransactionAttribute
                    m.CustomAttributes.Any(a => 
                        a.AttributeType.IsAssignableFrom(typeof(TransactionAttribute))))
                .With<TransactionInterceptor>();
            kernel.Bind<TransactionInterceptor>().ToSelf();
            kernel.Bind<IDao>().To<Dao>().InRequestScope();
            return kernel;
        }
    }
}
