using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using biblioteca2.Models;

namespace biblioteca2.Controllers
{
    public class UploadsController : Controller
    {
        //
        // GET: /Uploads/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string fileName = upload.FileName;

            string basePath = Server.MapPath("~/Files/Images/Tutos");
            upload.SaveAs(basePath + "\\" + fileName);

            return View();
        }
        public ActionResult Browse(string CKEditorFuncNum)
        {
            List<FileInformation> fileInfoList = GetCurrentFiles();

            var model = new FileListingViewModel
            {
                Files = fileInfoList,
                CKEditorFuncNum = CKEditorFuncNum
            };

            return View(model);
        }
        private List<FileInformation> GetCurrentFiles()
        {
            string basePath = Server.MapPath("~/Files/Images/Tutos");

            List<FileInformation> fileInfoList = new List<FileInformation>();

            string[] files = Directory.GetFiles(basePath);

            files.ToList().ForEach(file =>
            {
                fileInfoList.Add(new FileInformation { FileName = Path.GetFileName(file) });
            });
            return fileInfoList;
        }
    }
}
