using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace SLouple.MVC.Controllers
{
    public class ResourcesController : AdvancedController
    {
        public ActionResult Default()
        {
            return ReturnFile(null);
        }

        public ActionResult Get()
        {
            return ReturnFile("Content-Disposition");
        }

        public ActionResult Download()
        {
            return ReturnFile("application/octet-stream");
        }

        private ActionResult ReturnFile(string contentType)
        {
            LoadIP();
            string file = HttpUtility.UrlDecode(this.RouteData.Values["file"].ToString());
            FileInfo fi = new FileInfo(Server.MapPath(file));
            if (contentType == null)
            {
                string extension = fi.Extension.ToLower();
                extension = extension.Substring(1, extension.Length - 1);
                switch (extension)
                {
                    case "woff": contentType = "application/font-woff"; break;
                    default: contentType = MimeMapping.GetMimeMapping(file); break;
                }
                
            }
            Response.AddHeader("Accept-Ranges", "bytes");
            Response.Cache.SetExpires(DateTime.Now.AddMonths(1));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetLastModified(fi.LastWriteTimeUtc);
            
            if (Request.Headers["Origin"] != null)
            {
                Response.AppendHeader("Access-Control-Allow-Origin", Request.Headers["Origin"]);
                Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            }
            return File(file, contentType);
        }
    }
}