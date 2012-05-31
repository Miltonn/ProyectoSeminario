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
            ViewBag.Message = "Libros";
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult tutorial()
        {
            return View();
        }
        public ActionResult cursos()
        {
            return View();
        }
        [Authorize(Roles="Usuario")]
        public ActionResult login() {
            DataClasses2DataContext av = new DataClasses2DataContext();
            perfilusers p = new perfilusers();
            p = (from a in av.perfilusers where a.nombre == User.Identity.Name select a).ToArray()[0];
            ViewBag.list = p;
            return View();
        }
        
        public ActionResult libros() {
            DataClasses2DataContext av = new DataClasses2DataContext();
            perfilusers p = new perfilusers();
            p = (from a in av.perfilusers where a.nombre == User.Identity.Name select a).ToArray()[0];
            ViewBag.list = p;
            return View();
        }
        
        public ActionResult articulos()
        {
            DataClasses2DataContext av = new DataClasses2DataContext();
            perfilusers p = new perfilusers();
            p = (from a in av.perfilusers where a.nombre == User.Identity.Name select a).ToArray()[0];
            ViewBag.list = p;
            return View();
        }
        
        public ActionResult tutoriales()
        {
            DataClasses2DataContext av = new DataClasses2DataContext();
            perfilusers p = new perfilusers();
            p = (from a in av.perfilusers where a.nombre == User.Identity.Name select a).ToArray()[0];
            ViewBag.list = p;
            return View();
        }
        
        public ActionResult curso()
        {
            DataClasses2DataContext av = new DataClasses2DataContext();
            perfilusers p = new perfilusers();
            p = (from a in av.perfilusers where a.nombre == User.Identity.Name select a).ToArray()[0];
            ViewBag.list = p;
            return View();
        }
    }
}
