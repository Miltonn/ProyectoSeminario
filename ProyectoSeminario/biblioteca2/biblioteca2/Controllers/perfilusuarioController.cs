using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;
using System.IO;
using System.Drawing.Imaging;

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
        
    }
}
