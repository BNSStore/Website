using Quartz;
using SLouple.MVC.Main;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLouple.MVC.Controllers
{
    public class MainController : AdvancedController
    {
        public MainController()
        {
            subdomain = "Main";
        }

        public ActionResult Default()
        {
            return Home();
        }

        public ActionResult Home()
        {
            string title = "Home";
            Load(title);
            return View(title);
        }

        public ActionResult Support()
        {
            string title = "Support";
            Load(title);
            if (postData != null)
            {
                    string ticketTitle = postData.ContainsKey("title") ? Convert.ToString(postData["title"]) : "";
                    int categoryID = postData.ContainsKey("categoryID") ? Convert.ToInt32(postData["categoryID"]) : -1;
                    string comment = postData.ContainsKey("comment") ? Convert.ToString(postData["comment"]) : "";
                    int rating = postData.ContainsKey("rating") ? Convert.ToInt32(postData["rating"]) : -1;
                    string name = postData.ContainsKey("name") ? Convert.ToString(postData["name"]) : "";
                    string email = postData.ContainsKey("email") ? Convert.ToString(postData["email"]) : "";
                    string phone = postData.ContainsKey("phone") ? Convert.ToString(postData["phone"]) : "";
                    Ticket ticket = new Ticket(ticketTitle, categoryID, rating, comment, name, email, phone);
                    ticket.AddTicket();
                    return Content("success");
            }
            else
            {
                return View(title);
            }
            
        }

        public ActionResult Chat()
        {
            string title = "Chat";
            Load(title);
            return View(title);
        }

        public ActionResult HelloWorld()
        {
            string title = "HelloWorld";
            Load(title);
            return View(title);
        }
	}
}