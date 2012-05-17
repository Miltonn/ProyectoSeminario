using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace biblioteca2.Controllers
{
    public class comentarioController : Controller
    {
        //
        // GET: /comentario/
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

    }
}
