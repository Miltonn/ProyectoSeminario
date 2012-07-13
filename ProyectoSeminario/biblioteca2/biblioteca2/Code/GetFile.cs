using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageFiles.Code
{
    public class GetFile:ActionResult
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.WriteFile(context.HttpContext.Server.MapPath(Path));
        }

    }
}