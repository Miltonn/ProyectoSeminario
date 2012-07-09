using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trirand.Web.Mvc;
using biblioteca2.Models;
using System.Web.UI.WebControls;

namespace biblioteca2.Controllers
{
    public class gridController : Controller
    {
        //
        // GET: /grid/

        public ActionResult Index()
        {
            GridDemo grid = new GridDemo();
            setproperties(grid.grid);
            ViewBag.grid = grid.grid;
            return View();
        }
        public JsonResult cargardatos()
        {
            GridDemo grid = new GridDemo();
            DataClasses1DataContext db = new DataClasses1DataContext();
            
            return grid.grid.DataBind(db.perfil);
        }
        public void editardatos(perfil per)
        {
            GridDemo grid = new GridDemo();
            if(grid.grid.AjaxCallBackMode == AjaxCallBackMode.EditRow){
                DataClasses1DataContext db = new DataClasses1DataContext();
                perfil update = db.perfil.Where(a => a.idPerfil == per.idPerfil).First<perfil>();
                update.idPerfil = per.idPerfil;
                update.infraccion = per.infraccion;
                update.karma = per.karma;
                update.beneado = per.beneado;
                db.SubmitChanges();
            }
        }
        private void setproperties(JQGrid gr)
        {
            gr.Height = Unit.Pixel(200);
            gr.DataUrl = Url.Action("cargardatos");
            gr.EditUrl = Url.Action("editardatos");
            gr.ToolBarSettings.ShowAddButton = true;
            gr.ToolBarSettings.ShowEditButton = true;
            gr.ToolBarSettings.ShowEditButton = true;
        }
    }
}
