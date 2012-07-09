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
    public class comentController : Controller
    {
        //
        // GET: /coment/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult coment1() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var m = (from d in db.mode_coment select d).ToList();
            if (m != null)
            {
                var contenido = (from d in db.mode_coment select d).ToList();
                ViewBag.contenido = contenido;
                ViewBag.cat = (from c in db.mode_coment select c).ToList();
            }
            else
            {
                ViewBag.error = "No Existe Comentarios por Revisar";
            }
            return View();
        }
        public ActionResult coment(int id)
        {
            if (id == null)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                ///List<mode_coment> m = new List<mode_coment>();
                var m = (from d in db.mode_coment select d).ToList();
                if (m != null)
                {
                    ViewBag.contenido = (from d in db.mode_coment where d.idpublicacion == id select d).ToList();
                    ViewBag.cat = (from c in db.mode_coment select c).ToList();
                    var p = (from d in db.mode_coment where d.idpublicacion == id select d).ToList();
                    char[] delimiterChars = new char[]{ ' ', ',', '.', ';',':' };
                    string  ofensivo = p.ToString();
                    foreach (string ofen in ofensivo.Split(delimiterChars))
                    {
                        if (ofen == "carajo" || ofen == "mierda" || ofen == "sonso" || ofen == "chafa" || ofen == "puto")
                        {
                            ViewBag.img = "ofensivo";
                        }
                        else { ViewBag.img1 = "no ofensivo"; }
                    }
                }
                else
                {
                    ViewBag.error = "No Existe Comentarios por Revisar";
                }
                return View();
            }
            else {
                DataClasses1DataContext db = new DataClasses1DataContext();
                var contenido = (from d in db.mode_coment where d.idpublicacion == id select d).ToList();
                ViewBag.contenido = contenido;
                ViewBag.cat = (from c in db.mode_coment select c).ToList();

                return View();
            }
        }
                
        public ActionResult editcoment(int id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var visto = (from a in db.comentarios where a.idComentarios == id select a).Single();
                visto.vistobueno = "true";
                db.SubmitChanges();
            }
            return RedirectToAction ("coment","coment",id);
        }
        public ActionResult eliminarcomet(int id) {
            using(DataClasses1DataContext db=new DataClasses1DataContext()){
                ViewBag.p=(from c in db.comentarios where c.idPublicacion==id select c).ToList();
                //comentarios Delete = db.comentarios.Single(p => p.idPublicacion == id);
                foreach(var del in ViewBag.p)
                    db.comentarios.DeleteOnSubmit(del);
                db.SubmitChanges();
            }
            DataClasses1DataContext db1 = new DataClasses1DataContext();
            var s = (from m in db1.mode_coment select m.idpublicacion).Min();
            return RedirectToAction("coment1","coment");
        }
    }
}
