using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using biblioteca2.Models;

namespace biblioteca2.Controllers
{
    public class solisitudController : Controller
    {
        //
        // GET: /solisitud/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult solisitud1()
        {
            return View();
        }
        public ActionResult solisitud() {
            return View();
        }
        public ActionResult addsolisitud(soliadmin model)
        {
            Guid id = (Guid)Membership.GetUser().ProviderUserKey;
            model.UserId = id;
            model.regsolisitud(model);
            return RedirectToAction("http://localhost:4391/solisitud/solisitud1");
        }
        public ActionResult aceptsolisitud() {
            DataClasses1DataContext db =new DataClasses1DataContext();
            ViewBag.sol = (from s in db.solisitudes select s).ToList();
            if (ViewBag.sol != null)
            {
                ViewBag.sol = (from s in db.solisitudes select s).ToList();
            }
            else { ViewBag.sol = "Sin Solisitudes"; }
            return View();
        }
        public ActionResult aceptarsol(Guid id,soliadmin model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Guid idu = id;
            Guid idr=(from r in db.aspnet_Roles where r.RoleName=="Administrador" select r.RoleId).ToArray()[0];
            model.regroll(idu,idr);
            using (DataClasses1DataContext dbb = new DataClasses1DataContext())
            {
                ViewBag.p = (from c in dbb.solisitudes where c.userid == id select c).ToList();
                foreach (var del in ViewBag.p)
                    db.solisitudes.DeleteOnSubmit(del);
                db.SubmitChanges();
            }
            return View("http://localhost:4391/solisitud/aceptsolisitud");
        }

        public ActionResult rechazarsol(Guid id)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                ViewBag.p = (from c in db.solisitudes where c.userid == id select c).ToList();
                foreach (var del in ViewBag.p)
                    db.solisitudes.DeleteOnSubmit(del);
                db.SubmitChanges();
            }
            return RedirectToAction("http://localhost:4391/solisitud/aceptsolisitud");
        }
    }
}
