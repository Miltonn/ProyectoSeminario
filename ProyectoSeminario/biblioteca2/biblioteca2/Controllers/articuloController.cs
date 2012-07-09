using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;
using System.IO;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace biblioteca2.Controllers
{
    public class articuloController : Controller
    {
        //
        // GET: /articulo/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult articulopublic()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("articulo");
            }

            return View();
        }
         
        [HttpPost,ValidateInput(false)]
        public ActionResult articulopublic(string Titulo, HttpPostedFileBase Portada, string Contenido, string Categoria, string Descripcion, articulomodelo model)
        {
            if (ModelState.IsValid)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
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
                else
                {
                    model.Portada = "imagenxdefecto.jpg";
                }
                
                model.Contenido = Contenido;
                model.Categoria = Categoria;
                model.Descripcion = Descripcion;
                model.idusers = (Guid)Membership.GetUser().ProviderUserKey;
                string categori = model.Categoria;
                model.regpubli(model);
                int idUs = db.publicacion.Where(m => m.titulo == model.Titulo).Select(m => m.idPublicacion).ToArray()[0];
                //int idcat = db.categoria.Where(c => c.tipo == model.Categoria).Select(c => c.idCategoria).ToArray()[0];
                model.regarticulo(idUs);

                char[] delimiterChars = { ' ', ',', '.', ':' };
                string[] words = categori.Split(delimiterChars);
                DataClasses1DataContext db2 = new DataClasses1DataContext();
                List<categoria> lista = new List<categoria>();
                string tipo = "";
                //int cat3 = 0;
                foreach (var s in words)
                {
                    string ls = s.Trim();
                    if(db2.categoria.Where(a=>a.tipo==ls).Count() == 0){
                        lista.Add(new categoria() { tipo = ls });
                    }
                    
                }
                if(lista.ToList().Count>0){
                    db.categoria.InsertAllOnSubmit(lista);
                    db.SubmitChanges();
                    int cat = (from a in db.categoria where a.tipo == tipo select a.idCategoria).ToArray()[0];
                    model.regcategorizacion(idUs, cat);
                }
                if (Request.IsAjaxRequest())
                {
                    return PartialView("articulo3");
                }
                
            }
            else {
                if (Request.IsAjaxRequest())
                    return PartialView("articulo");
            }
            TempData["Message"] = string.Format("En hora buena ,{0}! se registro el articulo.",model.Titulo);
            return RedirectToAction("About","Home");
        }
        [Authorize(Roles="Usuario")]
        public ActionResult articulop() {
            return View();
        }
        [Authorize(Roles = "Usuario")]
        [HttpPost, ValidateInput(false)]
        public ActionResult articulop(string Titulo, HttpPostedFileBase Portada, string Contenido, string Categoria, string Descripcion, articulomodelo model)
        {
            if (ModelState.IsValid)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
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
                else
                {
                    model.Portada = "imagenxdefecto.jpg";
                }

                model.Contenido = Contenido;
                model.Categoria = Categoria;
                model.Descripcion = Descripcion;
                model.idusers = (Guid)Membership.GetUser().ProviderUserKey;
                string categori = model.Categoria;
                model.regpubli(model);
                int idUs = db.publicacion.Where(m => m.titulo == model.Titulo).Select(m => m.idPublicacion).ToArray()[0];
                //int idcat = db.categoria.Where(c => c.tipo == model.Categoria).Select(c => c.idCategoria).ToArray()[0];
                model.regarticulo(idUs);

                char[] delimiterChars = { ' ', ',', '.', ':' };
                string[] words = categori.Split(delimiterChars);
                DataClasses1DataContext db2 = new DataClasses1DataContext();
                List<categoria> lista = new List<categoria>();
                string tipo = "";
                //int cat3 = 0;
                foreach (var s in words)
                {
                    string ls = s.Trim();
                    if (db2.categoria.Where(a => a.tipo == ls).Count() == 0)
                    {
                        lista.Add(new categoria() { tipo = ls });
                    }

                }
                if (lista.ToList().Count > 0)
                {
                    db.categoria.InsertAllOnSubmit(lista);
                    db.SubmitChanges();
                    int cat = (from a in db.categoria where a.tipo == tipo select a.idCategoria).ToArray()[0];
                    model.regcategorizacion(idUs, cat);
                }
            }
            return RedirectToAction("articulop", "articulo");
        }
    }
}
