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
            ViewBag.l = (from l in db.libross select l).ToList();
            if (ViewBag.l != null)
            {
                ViewBag.libro = (from l in db.libross select l).ToList();
            }
            else { ViewBag.error = "Sin Datos q Mostrar"; }
            ViewBag.a = (from a in db.articuloss select a).ToList();
            if (ViewBag.a != null)
            {
                ViewBag.articulo = (from a in db.articuloss select a).ToList();
            }
            else { ViewBag.error = "Sin Datos q Mostrar"; }
            ViewBag.c = (from c in db.cursoss select c).ToList();
            if (ViewBag.c != null)
            {
                ViewBag.curso = (from c in db.cursoss select c).ToList();
            }
            else { ViewBag.error = "Sin Datos q Mostrar"; }
            ViewBag.t = (from t in db.tutorialess select t).ToList();
            if (ViewBag.t != null)
            {
                ViewBag.tutorial = (from t in db.tutorialess select t).ToList();
            }
            else { ViewBag.error = "Sin Datos q Mostrar"; }
            return View();
        }
        [Authorize(Roles="Cliente")]
        public ActionResult libross()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listalibro.ToList().Take(10).OrderByDescending(a => a.fecha_publicacion);
            if (lista != null)
            {
                ViewBag.lib = lista;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categorialibro select t).ToList();
            return View();
        }
        [Authorize(Roles = "Cliente")]
        public ActionResult About()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listaart.ToList().Take(10).OrderByDescending(a => a.fecha_publicacion);
            if (lista != null)
            {
                ViewBag.lib = lista;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categoria select t).ToList();
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
        [Authorize(Roles="Cliente")]
        public ActionResult libro()
        {
            
                DataClasses1DataContext db = new DataClasses1DataContext();
                publicacion pe = new publicacion();
                perfilusers u = new perfilusers();
                categoria c = new categoria();
                categorizacion cat = new categorizacion();
                pe = (from b in db.publicacion select b).ToArray()[0];
                //ViewBag.p = from b in db.publicacion select b;
                //ViewBag.u = from s in db.perfilusers select s;
                ViewBag.p = null;    
            return View();
            
        }
        [HttpPost]
        public ActionResult libro(string buscarr,buscar model)
        {
            
                DataClasses1DataContext db = new DataClasses1DataContext();
                //publicacion pe = new publicacion();
                perfilusers u = new perfilusers();
                categoria c = new categoria();
                categorizacion cat = new categorizacion();
                var pe = (from b in db.publicacion join l in db.libro on b.idPublicacion equals l.idPublicacion where b.idPublicacion == l.idPublicacion && b.titulo == buscarr && b.correcciones=="true" select b).ToList();
                if (pe != null)
                {
                    var lib = (from l in db.publicacion where l.titulo == buscarr select l).ToArray()[0];
                    ViewBag.p = (from l in db.publicacion where l.titulo == buscarr select l).ToList();
                    ViewBag.u = (from s in db.perfilusers where s.UserId == lib.UserId select s).ToList();
                    model.idPublicacion = (int)lib.idPublicacion;
                    model.UserId = lib.UserId;
                    comentario co = new comentario();
                    try
                    {
                        co = (from m in db.comentario where m.titulo == buscarr select m).ToArray()[0];
                        // if (co != null)
                        //{
                        ViewBag.comentario = from m in db.comentario where m.titulo == buscarr select m;
                    }
                    catch (Exception)
                    {
                        ViewBag.comentario2 = "Contenido sin comentarios";
                    }
                }
                else { ViewBag.error = "Sin contenido"; }
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
        [Authorize(Roles = "Cliente")]
        public ActionResult Cursos()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listcur.ToList().Take(10).OrderByDescending(a => a.fecha_publicacion);
            if (lista != null)
            {
                ViewBag.lib = lista;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categoria select t).ToList();
            return View();
        }
        [Authorize(Roles="Cliente")]
        public ActionResult login() {
            DataClasses1DataContext av = new DataClasses1DataContext();
            //perfilusers p = new perfilusers();
            var p = (from a in av.perfilusers where a.nombre == User.Identity.Name select a).ToList();
            ViewBag.list = p;
            ViewBag.li = av.listalibro.ToList().Take(10).OrderByDescending(a => a.fecha_publicacion);
            if (ViewBag.li != null)
            {
                ViewBag.lib = av.listalibro.ToList().Take(10).OrderByDescending(a => a.fecha_publicacion);
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            publicacion p1 = new publicacion();
            ViewBag.p1 = av.publicacion.ToList().Take(10).OrderByDescending(a => a.fecha_publicacion);
            ViewBag.tipo = (from t in av.categoria select t).ToList();
            return View();
        }

        public ActionResult megusta(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext()) { 
                Guid iid=(Guid)Membership.GetUser().ProviderUserKey;
                var pl=(from p in db.publicacion where p.idPublicacion==id select p).Single();
                var per=(from pp in db.perfil where pp.UserId==iid select pp).Single();
                pl.puntaje += 1;
                per.karma += 1;
                db.SubmitChanges();
            }
            return Redirect("http://localhost:4391/Home/About/"+id);
        }
        public ActionResult nomegusta(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var pl = (from p in db.publicacion where p.idPublicacion == id select p).Single();
                pl.puntaje -= 1;
                db.SubmitChanges();
            }
            return Redirect("http://localhost:4391/Home/About/"+id);
        }
        [Authorize(Roles = "Usuario")]
        public ActionResult libros() {
            return View();
        }
        [HttpPost]
        [Authorize(Roles="Usuario")]
        public ActionResult libros(string Titulo, HttpPostedFileBase Portada, HttpPostedFileBase contenido, string Descripcion, string Autor, string Idioma, string Tipo,string Año_Publicacion,string Tamaño, publicmodel model)
        {
            if (ModelState.IsValid)
            {
                //try
                
                    DataClasses1DataContext db = new DataClasses1DataContext();
                    //model.Titulo = Titulo;
                    model.Titulo = Titulo;
                    if (Portada != null)
                    {
                        var data2 = new byte[Portada.ContentLength];
                        Portada.InputStream.Read(data2, 0, Portada.ContentLength);
                        var path = ControllerContext.HttpContext.Server.MapPath("/Content/Imagenes/");
                        var filename = Path.Combine(path, Path.GetFileName(Portada.FileName));
                        System.IO.File.WriteAllBytes(Path.Combine(path, filename), data2);
                        model.Portada = (Portada.FileName).ToString();
                    }
                    else { model.Portada = "libroxd.jpg"; }
                    model.Autor = Autor;
                    model.Tipo = Tipo;
                    model.Idioma = Idioma;
                    if (contenido != null)
                    {
                        var data1 = new byte[contenido.ContentLength];
                        contenido.InputStream.Read(data1, 0, contenido.ContentLength);
                        var path1 = ControllerContext.HttpContext.Server.MapPath("/Content/ArchivoPDF/");
                        var filename1 = Path.Combine(path1, Path.GetFileName(contenido.FileName));
                        System.IO.File.WriteAllBytes(Path.Combine(path1, filename1), data1);
                    
                    model.Tamaño = (contenido.ContentLength).ToString();
                    model.idusers = (Guid)Membership.GetUser().ProviderUserKey;
                    model.Contenido = (contenido.FileName).ToString();
                    }
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
        [Authorize(Roles = "Cliente")]
        public ActionResult Tutorial() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listtut.ToList().Take(10).OrderByDescending(a => a.fecha_publicacion);
            if (lista != null)
            {
                ViewBag.lib = lista;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categoria select t).ToList();
            return View();
       }
        public ActionResult detallelibro(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listalibro.ToList().Where(a => a.idpublicacion==id);
            if (lista != null)
            {
                ViewBag.lib = lista;
                ViewBag.l = id;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            ViewBag.tipol = (from tip in db.categorialibro where tip.idpublicacion==id select tip).ToArray()[0];
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categorialibro select t).ToList();
            ViewBag.comentario = from m in db.comentario where m.idpublicacion == id select m;
            return View();
       }
        public ActionResult detallearticulo(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listaart.ToList().Where(a => a.idpublicacion == id);
            if (lista != null)
            {
                ViewBag.lib = lista;
                ViewBag.l= id;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            //ViewBag.tipol = (from tip in db.categoriaarticulo where tip.idpublicacion == id select tip).ToArray()[0];
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categoria select t).ToList();
            ViewBag.comentario = (from m in db.comentario where m.idpublicacion == id select m).ToList();
            return View();
        }
        public ActionResult detallecurso(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listcur.ToList().Where(a => a.idpublicacion == id);
            if (lista != null)
            {
                ViewBag.lib = lista;
                ViewBag.l = id;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            //ViewBag.tipol = (from tip in db.categoriacurso where tip.idpublicacion == id select tip).ToArray()[0];
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categoriacurso select t).ToList();
            ViewBag.comentario = from m in db.comentario where m.idpublicacion == id select m;
            return View();
        }
        public ActionResult detalletutorial(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var lista = db.listtut.ToList().Where(a => a.idpublicacion == id);
            if (lista != null)
            {
                ViewBag.lib = lista;
                ViewBag.l = id;
            }
            else { ViewBag.lib = "No se encontro ningun registro"; }
            //ViewBag.tipol = (from tip in db.categoriatutorial where tip.idpublicacion == id select tip).ToArray()[0];
            publicacion p = new publicacion();
            ViewBag.p = db.publicacion.ToList().Take(10).OrderByDescending(a => a.idPublicacion);
            ViewBag.tipo = (from t in db.categoriatutorial select t).ToList();
            ViewBag.comentario = from m in db.comentario where m.idpublicacion == id select m;
            return View();
        }
        public ActionResult curso() {
            return View();
        }
        
    }
}
