using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;
using System.IO;

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
            var data = new byte[Avatar.ContentLength];
            Avatar.InputStream.Read(data, 0, Avatar.ContentLength);
            var path = ControllerContext.HttpContext.Server.MapPath("/Content/imagenes/");
            var filename = Path.Combine(path, Path.GetFileName(Avatar.FileName));
            System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
            /*model.nombre = nombre;
            model.apellido = apellido;
            model.interes = interes;
            model.ubicacion = ubicacion;
            model.avatar = Path.GetFileName(Avatar.FileName).ToString();*/

            DataClasses1DataContext db = new DataClasses1DataContext();
            persona reg = new persona() { nombre = nombre, apellido = apellido, avatar = Avatar.FileName, interes = interes, ubicacion = ubicacion };
            db.persona.InsertOnSubmit(reg);
            db.SubmitChanges();
            
            return View();
        }
        
    }
}
