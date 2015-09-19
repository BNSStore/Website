using SLouple.MVC.Shared;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BNSStore.MVC.Controllers
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
            string filePath = HttpUtility.UrlDecode(this.RouteData.Values["file"].ToString());
            FileInfo fi = new FileInfo(Server.MapPath(filePath));
            if (contentType == null)
            {
                string extension = fi.Extension.ToLower();
                extension = extension.Substring(1, extension.Length - 1);
                switch (extension)
                {
                    case "woff": contentType = "application/font-woff"; break;
                    default: contentType = MimeMapping.GetMimeMapping(filePath); break;
                }
                
            }
            Response.AddHeader("Accept-Ranges", "bytes");
            Response.Cache.SetExpires(DateTime.Now.AddMonths(1));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetLastModified(fi.LastWriteTimeUtc);

            return File(filePath, contentType);
        }
    }
}