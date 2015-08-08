using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLouple.MVC.Controllers
{
    public class ErrorController : AdvancedController
    {
        //
        // GET: /Account/
        public ActionResult Default()
        {
            return Error();
        }

        public ActionResult Error()
        {
            string title = "Error";
            return View(title);
        }
    }
}