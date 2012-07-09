using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using biblioteca2.Models;

namespace biblioteca2.Controllers
{
    public class reedicionController : Controller
    {
        //
        // GET: /reedicion/

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="Usuario")]
        public ActionResult reedicion() {
            DataClasses1DataContext db =new DataClasses1DataContext();
            var libro = (from p in db.publicacion join l in db.libro on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.correcciones=="corregir" select p).ToList();
            var art = (from p in db.publicacion join a in db.articulo on p.idPublicacion equals a.idPublicacion where p.idPublicacion == a.idPublicacion && p.correcciones == "corregir" select p).ToList();
            var tut = (from p in db.publicacion join t in db.tutorial on p.idPublicacion equals t.idPublicacion where p.idPublicacion == t.idPublicacion && p.correcciones == "corregir" select p).ToList();
            var cur = (from p in db.publicacion join c in db.curso on p.idPublicacion equals c.idPublicacion where p.idPublicacion == c.idPublicacion && p.correcciones == "corregir" select p).ToList();
            if (libro != null)
            {
                ViewBag.libro = (from p in db.publicacion join l in db.libro on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.correcciones == "corregir" select p).ToList();
            }
            else { ViewBag.libro = "Sin libros q corregir"; }
            if (art != null)
            {
                ViewBag.art = (from p in db.publicacion join a in db.articulo on p.idPublicacion equals a.idPublicacion where p.idPublicacion == a.idPublicacion && p.correcciones == "corregir" select p).ToList();
            }
            else { ViewBag.demas = "No existen Articulo, Tutoriales o Cursos por corregir"; }
            if (tut != null)
            {
                ViewBag.tut = (from p in db.publicacion join t in db.tutorial on p.idPublicacion equals t.idPublicacion where p.idPublicacion == t.idPublicacion && p.correcciones == "corregir" select p).ToList();
            }
            else { ViewBag.demas1 = "No existen Articulo, Tutoriales o Cursos por corregir"; }
            if (cur != null)
            {
                ViewBag.cur = (from p in db.publicacion join c in db.curso on p.idPublicacion equals c.idPublicacion where p.idPublicacion == c.idPublicacion && p.correcciones == "corregir" select p).ToList();
            }
            else { ViewBag.demas2 = "No existen Articulo, Tutoriales o Cursos por corregir"; }
            return View();
        }
        public ActionResult editarlibro(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.libro = db.publicacion.Where(a => a.idPublicacion == id).Select(a => a).ToList();
            ViewBag.lib = (from p in db.libro where p.idPublicacion == id select p).ToList();
            return View();
        }

    }
}
