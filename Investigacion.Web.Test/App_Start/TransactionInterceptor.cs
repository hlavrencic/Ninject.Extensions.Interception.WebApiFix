using Investigacion.Web.Test.Services;
using Ninject.Extensions.Interception;

namespace Investigacion.Web.Test.App_Start
{
    public class TransactionInterceptor : IInterceptor
    {
        private readonly IDao dao;

        public TransactionInterceptor(IDao dao)
        {
            this.dao = dao;
        }

        public void Intercept(IInvocation invocation)
        {
            dao.Increment();
            invocation.Proceed();
        }
    }
}