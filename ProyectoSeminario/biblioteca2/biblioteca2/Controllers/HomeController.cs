using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;
using System.IO;
using System.Web.Security;


namespace biblioteca2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            vista v = new vista();
            ViewBag.li = from s in db.vista select s;
            if (ViewBag.li != null)
            {
                ViewBag.lib =  from s in db.vista select s; 
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            publicacion p = new publicacion();
            ViewBag.p = from pu in db.publicacion select pu;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult articulos()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            articulos art = new articulos();
            art = (from a in db.articulos select a).ToArray()[0];
            if (art != null)
            {
                ViewBag.arti = (from a in db.articulos select a).ToArray()[0];
                ViewBag.ca = (from c in db.categorias where c.idPublicacion == art.idpublicacion select c).ToArray()[0];
            }
            else { ViewBag.error = "No existe ningun registro"; }
            return View();
        }
        public ActionResult libro()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            publicacion pe = new publicacion();
            perfilusers u = new perfilusers();
            categoria c = new categoria();
            categorizacion cat = new categorizacion();
            pe = (from b in db.publicacion select b).ToArray()[0];
            ViewBag.p = from b in db.publicacion select b;
            ViewBag.u = from s in db.perfilusers select s;
            return View();
        }
        [HttpPost]
        public ActionResult libro(string buscarr,buscar model)
        {
            
                DataClasses1DataContext db = new DataClasses1DataContext();
                publicacion pe = new publicacion();
                perfilusers u = new perfilusers();
                categoria c = new categoria();
                categorizacion cat = new categorizacion();
                pe = (from b in db.publicacion where b.titulo == buscarr select b).ToArray()[0];
                ViewBag.p = from b in db.publicacion where b.titulo == buscarr select b;
                ViewBag.u = from s in db.perfilusers where s.UserId == pe.UserId select s;
                //ViewBag.cat = from ct in db.categorizacion where ct.idPublicacion == pe.idPublicacion select ct;
                //cat = (from ct in db.categorizacion where ct.idPublicacion == pe.idPublicacion select ct).ToArray()[0];
                //ViewBag.ca = from cc in db.categoria where cc.idCategoria == cat.idCategoria select cc;
                model.idPublicacion =(int)pe.idPublicacion;
                model.UserId=pe.UserId;
                comentario co = new comentario();
                try
                {
                    co = (from m in db.comentario where m.titulo == buscarr select m).ToArray()[0];
                // if (co != null)
                //{
                    ViewBag.comentario = from m in db.comentario where m.titulo == buscarr select m;
                }
                catch(Exception)
                {
                    ViewBag.comentario2 = "Contenido sin comentarios";
                }
                return View();
            
            //return View();
        }
        [HttpGet]
        public ActionResult comentarios(int bus,string Email, string Comentario, buscar model) 
        {
            model.idPublicacion = bus;
            model.UserId = (Guid)Membership.GetUser().ProviderUserKey;
            model.Email = Email;
            model.Comentario = Comentario;
            model.comentario(model);
            return RedirectToAction("libro","Home");
        }
        public ActionResult cursos()
        {
            return View();
        }
        [Authorize(Roles="Usuario")]
        public ActionResult login() {
            DataClasses1DataContext av = new DataClasses1DataContext();
            perfilusers p = new perfilusers();
            p = (from a in av.perfilusers where a.nombre == User.Identity.Name select a).ToArray()[0];
                ViewBag.list = p;
            
            vista v = new vista();
            ViewBag.li = from s in av.vista select s;
            if (ViewBag.li != null)
            {
                ViewBag.lib = from s in av.vista select s;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            publicacion p1 = new publicacion();
            ViewBag.p1 = from pu in av.publicacion select pu;
            return View();
        }

        public ActionResult libros()
        {
            
            return View();
        }
        [HttpPost]
        [Authorize(Roles="Usuario")]
        public ActionResult libros(string Titulo, HttpPostedFileBase Portada, HttpPostedFileBase contenido, string Descripcion, string Autor, string Idioma, string Tipo,string Año_Publicacion,string Tamaño, publicmodel model)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                    DataClasses1DataContext db = new DataClasses1DataContext();
                    //model.Titulo = Titulo;
                    model.Titulo = Titulo;
                     var data2 = new byte[Portada.ContentLength];
                     Portada.InputStream.Read(data2, 0, Portada.ContentLength);
                     var path = ControllerContext.HttpContext.Server.MapPath("/Content/Imagenes/");
                     var filename = Path.Combine(path, Path.GetFileName(Portada.FileName));
                     System.IO.File.WriteAllBytes(Path.Combine(path, filename), data2);
                    model.Portada = (Portada.FileName).ToString();
                    model.Autor = Autor;
                    model.Tipo = Tipo;
                    model.Idioma = Idioma;
                   
                    var data1 = new byte[contenido.ContentLength];
                    contenido.InputStream.Read(data1, 0, contenido.ContentLength);
                    var path1 = ControllerContext.HttpContext.Server.MapPath("/Content/ArchivoPDF/");
                    var filename1 = Path.Combine(path, Path.GetFileName(contenido.FileName));
                    System.IO.File.WriteAllBytes(Path.Combine(path, filename), data1);
                    model.Tamaño = (contenido.ContentLength).ToString();
                    model.idusers = (Guid)Membership.GetUser().ProviderUserKey;
                    model.Contenido = (contenido.FileName).ToString();
                    model.Año_Publicacion = Año_Publicacion;
                    model.Descripcion = Descripcion;
                    model.regpubli(model);

                    int idUs = db.publicacion.Where(m => m.titulo == model.Titulo).Select(m => m.idPublicacion).ToArray()[0];
                    model.reglibro(model, idUs);

                    model.regcategoria(model);

                    int idcat = db.categoria.Where(c => c.tipo == model.Tipo).Select(c => c.idCategoria).ToArray()[0];
                    model.regcategorizacion(idUs, idcat);
                
            }
            
            return View();
        }
       
    }
}
