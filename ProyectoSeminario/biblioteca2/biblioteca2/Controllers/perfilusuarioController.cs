using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;
using System.IO;
using System.Drawing.Imaging;
using System.Web.Security;

namespace biblioteca2.Controllers
{
    public class perfilusuarioController : Controller
    {
        //
        // GET: /perfilusuario/

        public ActionResult Index()
        {
            return View();
        }
        /*public ActionResult perfil() {
            return View();
        }*/
        [HttpGet]
        public ActionResult perfil()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("perfilusuario");
            }

            return View();
        }
        [HttpPost]
        public ActionResult perfil(string nombre, string apellido, HttpPostedFileBase Avatar, perfilext model, string ubicacion, string interes)
        {
            if (ModelState.IsValid){
                if(Avatar != null){
                    var data = new byte[Avatar.ContentLength];
                    Avatar.InputStream.Read(data, 0, Avatar.ContentLength);
                    var path = ControllerContext.HttpContext.Server.MapPath("/Content/imagenes/");
                    var filename = Path.Combine(path, Path.GetFileName(Avatar.FileName));
                    System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
                    model.Avatar = Avatar.FileName;
                }else{
                    string imagen="MrX.png";
                    model.Avatar = imagen;
                }
                model.Nombre = nombre;
                model.Apellido = apellido;
                model.Ubicacion = ubicacion;
                model.Interes = interes;
                DataClasses1DataContext db = new DataClasses1DataContext();
                System.Guid idUs = db.aspnet_Users.Where(a => a.UserName == User.Identity.Name).Select(a => a.UserId).ToArray()[0];
                System.Guid idRol = db.aspnet_Roles.Where(a => a.RoleName == "Usuario").Select(a => a.RoleId).ToArray()[0];
                model.regperfilusers(model, idUs);
                model.regperfil(idUs);
                model.regroles(idUs,idRol);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("perfilusuario3");
                }
            }
            else
            {
                if (Request.IsAjaxRequest()) 
                return PartialView("perfilusuario");
            }
            TempData["Message"] = string.Format("Bienvenidos, {0}! eres un nuevo usuario.");
            return RedirectToAction("Index");
          
       }
        [Authorize(Roles="Cliente")]
        public ActionResult detalle() 
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Guid id = (Guid)Membership.GetUser().ProviderUserKey;
            ViewBag.coment = (from c in db.comentarios where c.UserId == id select c).ToList();
            if (ViewBag.coment != null)
            {
                //ViewBag.publi = (from p in db.publicacion where p.UserId == id select p).ToList();
                ViewBag.coment = (from c in db.comentarios where c.UserId == id select c).ToList();
                ViewBag.datos = (from f in db.perfilusers where f.UserId == id select f).ToList();
                ViewBag.perfil = (from d in db.perfil where d.UserId == id select d).ToList();
            }
            else { ViewBag.coment = "Usted no tiene Comentarios"; }
            return View();
        }
        [Authorize(Roles = "Usuario")]
        public ActionResult detallelibro()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Guid id = (Guid)Membership.GetUser().ProviderUserKey;
            ViewBag.publi = (from p in db.publicacion join l in db.libro on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id  select p).ToList();
            if (ViewBag.publi != null)
            {
                ViewBag.publi = (from p in db.publicacion join l in db.libro on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id select p).ToList();
                ViewBag.datos = (from f in db.perfilusers where f.UserId == id select f).ToList();
                ViewBag.perfil = (from d in db.perfil where d.UserId == id select d).ToList();
            }
            else { ViewBag.publi = "Usted no tiene Contenidos de libros publicados"; }
            return View();
        }
        [Authorize(Roles = "Usuario")]
        public ActionResult detallearticulo()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Guid id = (Guid)Membership.GetUser().ProviderUserKey;
            ViewBag.publi = (from p in db.publicacion join l in db.articulo on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id select p).ToList();
            if (ViewBag.publi != null)
            {
                ViewBag.publi = (from p in db.publicacion join l in db.articulo on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id select p).ToList();
                ViewBag.datos = (from f in db.perfilusers where f.UserId == id select f).ToList();
                ViewBag.perfil = (from d in db.perfil where d.UserId == id select d).ToList();
            }
            else { ViewBag.publi = "Usted no tiene Contenidos de libros publicados"; }
            return View();
        }
        [Authorize(Roles = "Usuario")]
        public ActionResult detalletutorial()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Guid id = (Guid)Membership.GetUser().ProviderUserKey;
            ViewBag.publi = (from p in db.publicacion join l in db.tutorial on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id select p).ToList();
            if (ViewBag.publi != null)
            {
                ViewBag.publi = (from p in db.publicacion join l in db.tutorial on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id select p).ToList();
                ViewBag.datos = (from f in db.perfilusers where f.UserId == id select f).ToList();
                ViewBag.perfil = (from d in db.perfil where d.UserId == id select d).ToList();
            }
            else { ViewBag.publi = "Usted no tiene Contenidos de libros publicados"; }
            return View();
        }
        [Authorize(Roles = "Usuario")]
        public ActionResult detallecurso()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Guid id = (Guid)Membership.GetUser().ProviderUserKey;
            ViewBag.publi = (from p in db.publicacion join l in db.curso on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id select p).ToList();
            if (ViewBag.publi != null)
            {
                ViewBag.publi = (from p in db.publicacion join l in db.curso on p.idPublicacion equals l.idPublicacion where p.idPublicacion == l.idPublicacion && p.UserId == id select p).ToList();
                ViewBag.datos = (from f in db.perfilusers where f.UserId == id select f).ToList();
                ViewBag.perfil = (from d in db.perfil where d.UserId == id select d).ToList();
            }
            else { ViewBag.publi = "Usted no tiene Contenidos de libros publicados"; }
            return View();
        }
    }
}
