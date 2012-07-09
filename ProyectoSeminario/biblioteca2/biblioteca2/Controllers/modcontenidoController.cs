using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;

namespace biblioteca2.Controllers
{
    public class modcontenidoController : Controller
    {
        //
        // GET: /modcontenido/

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="Administrador")]
        public ActionResult detalle(int id) 
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            
            return View();
        }
        
        public ActionResult modcontenido(int id)
        {
            
            DataClasses1DataContext db = new DataClasses1DataContext();
            var libroo = (from l in db.libro where l.idPublicacion ==id select l).ToArray()[0];
            var publi = (from p in db.publicacion where p.idPublicacion == libroo.idPublicacion select p).ToList();
            if (libroo != null)
            {
                ViewBag.pub = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion select o).ToList();
                ViewBag.publi = (from p in db.publicacion where p.idPublicacion == id select p).ToList();
                ViewBag.libro = (from l in db.libro where l.idPublicacion == id select l).ToArray()[0];
                ViewBag.sms = "libro";
                return View();
            }
            else {
                 ViewBag.error = "Sin libros";
                 return View();
            }
                        
        }
        public ActionResult modarticulo(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            
            var articulo = (from a in db.articulo where a.idPublicacion == id select a).ToArray()[0];
            var publi = (from p in db.publicacion where p.idPublicacion == articulo.idPublicacion select p).ToList();
            if (articulo != null)
            {
                ViewBag.pub = (from o in db.publicacion join a in db.articulo on  o.idPublicacion equals a.idPublicacion where o.idPublicacion == a.idPublicacion select o).ToList();
                ViewBag.publi = (from p in db.publicacion where p.idPublicacion == id select p).ToList();
                ViewBag.articulo = (from a in db.articulo where a.idPublicacion == articulo.idPublicacion select a).ToList();
                ViewBag.sms = "articulo";
                return View();
            }
            else { ViewBag.error = "Sin Articulos";
            return View();
            }
            
        }
        public ActionResult modcurso(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var publi = (from p in db.publicacion where p.idPublicacion == id select p).ToArray()[0];
            var curso = (from c in db.curso where c.idPublicacion == publi.idPublicacion select c).ToArray()[0];
            if (curso != null)
            {
                ViewBag.pub = (from o in db.publicacion join a in db.curso on o.idPublicacion equals a.idPublicacion where o.idPublicacion == a.idPublicacion select o).ToList();
                ViewBag.publi = (from p in db.publicacion where p.idPublicacion == id select p).ToList();
                ViewBag.curso = (from c in db.curso where c.idPublicacion == publi.idPublicacion select c).ToList();
                ViewBag.sms = "curso";
                return View();
            }
            else { ViewBag.error = "Sin Cursos"; return View(); }
        }
        public ActionResult modtutorial(int id)
        { 
            DataClasses1DataContext db = new DataClasses1DataContext();
            var publi = (from p in db.publicacion where p.idPublicacion == id select p).ToArray()[0];
            var tutorial = (from t in db.tutorial where t.idPublicacion == publi.idPublicacion select t).ToArray()[0];
            if (tutorial != null)
            {
                ViewBag.pub = (from o in db.publicacion join a in db.tutorial on o.idPublicacion equals a.idPublicacion where o.idPublicacion == a.idPublicacion select o).ToList();
                ViewBag.publi = (from p in db.publicacion where p.idPublicacion == id select p).ToList();
                ViewBag.tutorial = (from t in db.tutorial where t.idPublicacion == publi.idPublicacion select t).ToList();
                ViewBag.sms = "tutorial";
                return View();
            }
            else { ViewBag.error = "Sin Cursos"; return View(); }
        }
        [Authorize(Roles="Administrador")]
        public ActionResult mod()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.pub1 = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion && o.correcciones=="false" select o).ToList();
            ViewBag.pub2 = (from o in db.publicacion join a in db.articulo on o.idPublicacion equals a.idPublicacion where o.idPublicacion == a.idPublicacion && o.correcciones == "false" select o).ToList();
            ViewBag.pub3 = (from o in db.publicacion join a in db.curso on o.idPublicacion equals a.idPublicacion where o.idPublicacion == a.idPublicacion && o.correcciones == "false" select o).ToList();
            ViewBag.pub4 = (from o in db.publicacion join a in db.tutorial on o.idPublicacion equals a.idPublicacion where o.idPublicacion == a.idPublicacion && o.correcciones == "false" select o).ToList();
            return View();
        }
       
        public ActionResult corregidol(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                        //var visto = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion && o.correcciones == "false" select o).Single();
                        var visto = (from p in db.publicacion where p.idPublicacion==id select p).Single();
                        var karma = (from o in db.perfil where o.UserId == visto.UserId select o).Single();
                        //var kar = (from o in db.perfil where o.idPublicacion == id select o).Single();
                        visto.correcciones = "true";
                        karma.karma +=  100;
                        db.SubmitChanges();
            }
                
             return Redirect("/modcontenido/mod");
        }
        public ActionResult corregidoa(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var visto = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion && o.correcciones == "false" select o).Single();
                var visto = (from p in db.publicacion where p.idPublicacion == id select p).Single();
                var karma = (from o in db.perfil where o.UserId == visto.UserId select o).Single();
                //var kar = (from o in db.perfil where o.idPublicacion == id select o).Single();
                visto.correcciones = "true";
                karma.karma += 10;
                db.SubmitChanges();
            }

            return Redirect("/modcontenido/mod");
        }
        public ActionResult corregidoc(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var visto = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion && o.correcciones == "false" select o).Single();
                var visto = (from p in db.publicacion where p.idPublicacion == id select p).Single();
                var karma = (from o in db.perfil where o.UserId == visto.UserId select o).Single();
                //var kar = (from o in db.perfil where o.idPublicacion == id select o).Single();
                visto.correcciones = "true";
                karma.karma += 200;
                db.SubmitChanges();
            }

            return Redirect("/modcontenido/mod");
        }
        public ActionResult corregidot(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var visto = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion && o.correcciones == "false" select o).Single();
                var visto = (from p in db.publicacion where p.idPublicacion == id select p).Single();
                var karma = (from o in db.perfil where o.UserId == visto.UserId select o).Single();
                //var kar = (from o in db.perfil where o.idPublicacion == id select o).Single();
                visto.correcciones = "true";
                karma.karma += 50;
                db.SubmitChanges();
            }

            return Redirect("/modcontenido/mod");
        }
        public ActionResult corregir(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                //var visto = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion && o.correcciones == "false" select o).Single();
                var visto = (from p in db.publicacion where p.idPublicacion == id select p).Single();
                //var karma = (from o in db.perfil where o.UserId == visto.UserId select o).Single();
                //var kar = (from o in db.perfil where o.idPublicacion == id select o).Single();
                visto.correcciones = "corregir";
                //karma.karma += 50;
                db.SubmitChanges();
            }

            return Redirect("/modcontenido/mod");
        }
        [Authorize(Roles="Administrador")]
        public ActionResult usuario(){
            DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.pu = (from p in db.perfilusers  select p).ToList();
            ViewBag.per = (from pr in db.perfil select pr).ToList();
            return View();
        }
        public ActionResult detalleuser(Guid userid) {
            DataClasses1DataContext db=new DataClasses1DataContext();
            ViewBag.per=(from p in db.perfil where p.UserId==userid select p).ToList();
            ViewBag.pub1 = (from o in db.publicacion join l in db.libro on o.idPublicacion equals l.idPublicacion where o.idPublicacion == l.idPublicacion && o.UserId == userid select o).ToList();
            ViewBag.pub2 = (from p in db.publicacion join a in db.articulo on p.idPublicacion equals a.idPublicacion where p.idPublicacion == a.idPublicacion && p.UserId == userid select p).ToList();
            ViewBag.pub3 = (from p1 in db.publicacion join c in db.curso on p1.idPublicacion equals c.idPublicacion where p1.idPublicacion == c.idPublicacion && p1.UserId == userid select p1).ToList();
            ViewBag.pub4 = (from p2 in db.publicacion join t in db.tutorial on p2.idPublicacion equals t.idPublicacion where p2.idPublicacion == t.idPublicacion && p2.UserId == userid select p2).ToList();
            ViewBag.pu = (from u in db.perfilusers where u.UserId == userid select u).ToList();
            return View();
        }
    }
}
