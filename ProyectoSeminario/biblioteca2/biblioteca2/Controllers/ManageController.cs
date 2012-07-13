using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ManageFiles.Code;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Hosting;
using biblioteca2.Models;

namespace ManageFiles.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);//obtenemos el nombre del archivo a cargar         
            file.SaveAs(Server.MapPath(@"~\Content\" + fileName));//guardamos el archivo en la ruta física que corresponde a la ruta virtual del archivo
            //Response.Write(file.FileName);
            //ShowFiles();
            return RedirectToAction("Index");//volvemos a la página principal
        }
        public GetFile DownloadFile(int id)
         {
             DataClasses1DataContext db = new DataClasses1DataContext();
             var pu = (from p in db.publicacion where p.idPublicacion == id select p).ToArray()[0];
             return new GetFile
             {
                 FileName = pu.contenido,
                 Path = @"~/Content/ArchivoPDF/" + pu.contenido

             };
         }
        

        //public void ShowFiles() 
        //{

        //    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Content"));
        //    int i = 0;
        //    foreach (FileInfo fi in di.GetFiles())
        //    {
        //        HyperLink HL = new HyperLink();
        //        HL.ID = "HyperLink" + i++;
        //        HL.Text = fi.Name;
        //        HL.NavigateUrl = "DownloadFile.aspx.aspx?file=" + fi.Name;

        //    }
        
        //}
    }
}
