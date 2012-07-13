using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using biblioteca2.Models;
using System.IO;

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
        [ValidateInput(false)]
        public ActionResult editararticulo(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.libro = db.publicacion.Where(a => a.idPublicacion == id).Select(a => a).ToList();
            //ViewBag.lib = (from p in db.libro where p.idPublicacion == id select p).ToList();
            return View();
        }
        [ValidateInput(false)]
        public ActionResult editarcurso(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.libro = db.publicacion.Where(a => a.idPublicacion == id).Select(a => a).ToList();
            //ViewBag.lib = (from p in db.libro where p.idPublicacion == id select p).ToList();
            return View();
        }
        [ValidateInput(false)]
        public ActionResult editartutorial(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.libro = db.publicacion.Where(a => a.idPublicacion == id).Select(a => a).ToList();
            //ViewBag.lib = (from p in db.libro where p.idPublicacion == id select p).ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Usuario")]
        public ActionResult actualizar(int idPublicacion, string Titulo, HttpPostedFileBase Portada, HttpPostedFileBase Contenido, string Descripcion, publicmodel model)
        {
            DataClasses1DataContext db=new DataClasses1DataContext();
            using(DataClasses1DataContext dd=new DataClasses1DataContext()){
                var libro = (from l in dd.publicacion where l.idPublicacion == idPublicacion select l).Single();
                libro.titulo = Titulo;
                if (Portada != null)
                {
                    var data = new byte[Portada.ContentLength];
                    Portada.InputStream.Read(data, 0, Portada.ContentLength);
                    var path = ControllerContext.HttpContext.Server.MapPath("~/Content/Imagenes/");
                    var filename = Path.Combine(path, Path.GetFileName(Portada.FileName));
                    System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
                    libro.portada = (Portada.FileName).ToString();
                }
                else { libro.portada = libro.portada; }
                if (Contenido != null)
                {
                    var data2 = new byte[Contenido.ContentLength];
                    Portada.InputStream.Read(data2, 0, Contenido.ContentLength);
                    var path1 = ControllerContext.HttpContext.Server.MapPath("~/Content/ArchivoPDF/");
                    var filename1 = Path.Combine(path1, Path.GetFileName(Contenido.FileName));
                    System.IO.File.WriteAllBytes(Path.Combine(path1, filename1), data2);
                    libro.contenido = (Contenido.FileName).ToString();
                }
                else { libro.contenido = libro.contenido; }
                libro.correcciones = "false";
                libro.descripcion = Descripcion;
                dd.SubmitChanges();
            }
            return RedirectToAction("reedicion", "reedicion");
        }

        [Authorize(Roles = "Usuario")]
        [HttpPost, ValidateInput(false)]
        public ActionResult actualizara(int idPublicacion, string Titulo, HttpPostedFileBase Portada, string Contenido, string Descripcion, articulomodelo model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            using (DataClasses1DataContext dd = new DataClasses1DataContext())
            {
                var libro = (from l in dd.publicacion where l.idPublicacion == idPublicacion select l).Single();
                libro.titulo = Titulo;
                if (Portada != null)
                {
                    var data = new byte[Portada.ContentLength];
                    Portada.InputStream.Read(data, 0, Portada.ContentLength);
                    var path = ControllerContext.HttpContext.Server.MapPath("~/Content/Imagenes/");
                    var filename = Path.Combine(path, Path.GetFileName(Portada.FileName));
                    System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
                    libro.portada = (Portada.FileName).ToString();
                }
                else { libro.portada = libro.portada; }
                libro.descripcion = Descripcion;
                libro.contenido = Contenido;
                libro.correcciones = "false";
                dd.SubmitChanges();
            }
            return RedirectToAction("reedicion", "reedicion");
        }
        [Authorize(Roles = "Usuario")]
        [HttpPost, ValidateInput(false)]
        public ActionResult actualizarc(int idPublicacion, string Titulo, HttpPostedFileBase Portada, string Contenido, string Descripcion, cursos model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            using (DataClasses1DataContext dd = new DataClasses1DataContext())
            {
                var libro = (from l in dd.publicacion where l.idPublicacion == idPublicacion select l).Single();
                libro.titulo = Titulo;
                if (Portada != null)
                {
                    var data = new byte[Portada.ContentLength];
                    Portada.InputStream.Read(data, 0, Portada.ContentLength);
                    var path = ControllerContext.HttpContext.Server.MapPath("~/Content/Imagenes/");
                    var filename = Path.Combine(path, Path.GetFileName(Portada.FileName));
                    System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
                    libro.portada = (Portada.FileName).ToString();
                }
                else { libro.portada = libro.portada; }
                libro.descripcion = Descripcion;
                libro.contenido = Contenido;
                libro.correcciones = "false";
                dd.SubmitChanges();
            }
            return RedirectToAction("reedicion", "reedicion");
        }
        [Authorize(Roles = "Usuario")]
        [HttpPost, ValidateInput(false)]
        public ActionResult actualizart(int idPublicacion, string Titulo, HttpPostedFileBase Portada, string Contenido, string Descripcion, tutoriales model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            using (DataClasses1DataContext dd = new DataClasses1DataContext())
            {
                var libro = (from l in dd.publicacion where l.idPublicacion == idPublicacion select l).Single();
                libro.titulo = Titulo;
                if (Portada != null)
                {
                    var data = new byte[Portada.ContentLength];
                    Portada.InputStream.Read(data, 0, Portada.ContentLength);
                    var path = ControllerContext.HttpContext.Server.MapPath("~/Content/Imagenes/");
                    var filename = Path.Combine(path, Path.GetFileName(Portada.FileName));
                    System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
                    libro.portada = (Portada.FileName).ToString();
                }
                else { libro.portada = libro.portada; }
                libro.descripcion = Descripcion;
                libro.contenido = Contenido;
                libro.correcciones = "false";
                dd.SubmitChanges();
            }
            return RedirectToAction("reedicion", "reedicion");
        }
    }
}
