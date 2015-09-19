using SLouple.MVC.Shared;
using System.Web.Mvc;

namespace BNSStore.MVC.Controllers
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