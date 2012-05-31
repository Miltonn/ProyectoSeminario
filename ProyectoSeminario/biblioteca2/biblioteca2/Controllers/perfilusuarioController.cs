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
        public ActionResult perfil() {
            return View();
        }
        [HttpPost]
        public ViewResult perfil(string nombre, string apellido, HttpPostedFileBase Avatar, perfil model, string ubicacion, string interes)
        {
            if (ModelState.IsValid){
            var data = new byte[Avatar.ContentLength];
            Avatar.InputStream.Read(data, 0, Avatar.ContentLength);
            var path = ControllerContext.HttpContext.Server.MapPath("/Content/imagenes/");
            var filename = Path.Combine(path, Path.GetFileName(Avatar.FileName));
            System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
            //if (Avatar.Equals(ImageFormat.Jpeg)||Avatar.Equals(ImageFormat.Png))
            //{
                DataClasses2DataContext db = new DataClasses2DataContext();
                //perfilusers p = new perfilusers();
                //p = (from a in db.perfilusers where a.nombre == User.Identity.Name select a).ToArray()[0];
                System.Guid idRol = db.aspnet_Users.Where(a => a.UserName == "ivan").Select(a => a.UserId).ToArray()[0];
                //System.Guid idUs = db.aspnet_Users.Where(a => a.UserName == model.UserName).Select(a => a.UserId).ToArray()[0];
                perfilusers reg = new perfilusers() { nombre = nombre, apellido = apellido, avatar = Avatar.FileName, ubicacion = ubicacion, interes = interes,UserId=idRol};
                db.perfilusers.InsertOnSubmit(reg);
                db.SubmitChanges();
               /* perfil rg = new perfil() { infraccion = 0, karma = 0, beneado = "true", UserId = p.UserId };
                db.perfil.InsertOnSubmit(rg);
                db.SubmitChanges();
                System.Guid idRol = db.aspnet_Roles.Where(a => a.RoleName == "Usuario").Select(a => a.RoleId).ToArray()[0];
                aspnet_UsersInRoles rel = new aspnet_UsersInRoles() { RoleId = idRol, UserId = p.UserId };
                db.aspnet_UsersInRoles.InsertOnSubmit(rel);
                db.SubmitChanges();*/
                ViewBag.er = "datos insertados correctamente";
                return View();
            }
            else {
                return View();
                ViewBag.er = "verifique los datos";
            }
        }
        
    }
}
