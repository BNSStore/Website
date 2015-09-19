using System.Web.Mvc;
using SLouple.MVC.Shared;

namespace BNSStore.MVC.Controllers
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