using Investigacion.Web.Test.App_Start;
using Investigacion.Web.Test.Services;

namespace Investigacion.Web.Test.Controllers
{
    public interface IServiceExample
    {
        void Save();
    }

    public class ServiceExample : IServiceExample
    {
        private readonly IDao dao;

        public ServiceExample(IDao dao)
        {
            this.dao = dao;
        }

        [Transaction]
        public virtual void Save()
        {
            dao.Increment();
        }
    }
}