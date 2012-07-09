﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biblioteca2.Models;
using System.Web.Security;
using System.IO;

namespace biblioteca2.Controllers
{
    public class cursoController : Controller
    {
        //
        // GET: /curso/

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="Usuario")]
        public ActionResult curso()
        {
            return View();
        }
        [Authorize(Roles = "Usuario")]
        [HttpPost, ValidateInput(false)]
        public ActionResult curso(string Titulo, HttpPostedFileBase Portada, string Contenido, string Categoria, string Descripcion, cursos model)
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
                int idd = idUs;
                model.regcurso(idUs);

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
                    int cat = db.categoria.Where(a => a.tipo == tipo).Select(a=>a.idCategoria).ToArray()[0];
                    model.regcategorizacionc(idd, cat);   
                }
                
            }
            return RedirectToAction("curso","curso");
        }
    }
}
