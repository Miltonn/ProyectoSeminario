using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace biblioteca2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "BIENVENIDO USUARIO";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
       
    }
}
