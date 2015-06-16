using Quartz;
using SLouple.MVC.Store;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace SLouple.MVC.Controllers
{
    public class CoshController : AdvancedController
    {
        public CoshController()
        {
            subdomain = "Cosh";
        }

        public ActionResult Default()
        {
            return Grow();
        }

        public ActionResult Blog()
        {
            string title = "Blog";
            Load(title);
            return View(title);
        }

        public ActionResult Grow()
        {
            string title = "Grow";
            Load(title);
            return View(title);
        }


    }

    
}