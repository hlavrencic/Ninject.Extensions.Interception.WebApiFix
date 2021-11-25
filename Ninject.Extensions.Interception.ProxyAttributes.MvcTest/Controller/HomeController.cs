using Microsoft.AspNetCore.Mvc;
using System;

namespace Ninject.Extensions.Interception.ProxyAttributes.MvcTest
{
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() 
        {
            return Json(DateTime.Now);
        }

    }
}