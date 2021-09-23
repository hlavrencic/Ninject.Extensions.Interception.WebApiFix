using Investigacion.Web.Test.Services;
using System.Web.Http;

namespace Investigacion.Web.Test.Controllers
{
    public class ItemController : ApiController
    {
        private readonly IServiceExample serviceExample;
        private readonly IDao dao;

        public ItemController(IServiceExample serviceExample, IDao dao)
        {
            this.serviceExample = serviceExample;
            this.dao = dao;
        }

        public int Get()
        {
            serviceExample.Save();

            return dao.Count;
        }

    }
}
