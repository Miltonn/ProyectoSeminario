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
            return RedirectToAction("solisitud1", "solisitud");
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
        public ActionResult aceptarsol(Guid UserId,soliadmin model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Guid idu = UserId;
            Guid idr=(from r in db.aspnet_Roles where r.RoleName=="Administrador" select r.RoleId).ToArray()[0];
            model.regroll(idu,idr);
            using (DataClasses1DataContext dbb = new DataClasses1DataContext())
            {
                ViewBag.p = (from c in dbb.solisitud where c.UserId == UserId select c).ToList();
                foreach (var del in @ViewBag.p)
                    db.solisitud.DeleteOnSubmit(del);
                db.SubmitChanges();
            }
            return RedirectToAction("aceptsolisitud", "solisitud");
        }

        public ActionResult rechazarsol(Guid UserId)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                ViewBag.p = (from c in db.solisitud where c.UserId == UserId select c).ToList();
                foreach (var del in @ViewBag.p)
                    db.solisitud.DeleteOnSubmit(del);
                db.SubmitChanges();
            }
            return RedirectToAction("aceptsolisitud", "solisitud");
        }
    }
}
