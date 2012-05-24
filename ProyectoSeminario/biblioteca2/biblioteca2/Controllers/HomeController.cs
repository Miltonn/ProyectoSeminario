using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;

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
        public ActionResult login() {
            persona p = new persona();
            DataClasses1DataContext av = new DataClasses1DataContext();
            p = (from a in av.persona where a.nombre == User.Identity.Name select a).ToArray()[0];
            ViewBag.list = p;
            return View();
        }
    }
}
