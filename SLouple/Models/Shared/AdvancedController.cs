using SLouple.MVC.Account;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLouple.MVC.Shared
{
    public class AdvancedController : Controller
    {
        public string subdomain;
        public Lang lang;
        public User user;
        public string ip;
        public Dictionary<string, string> ids;
        public Dictionary<string, string> postData;

        [NonAction]
        public void Load(string title)
        {
            LoadIP();
            LoadTitle(title);
            LoadIds();
            LoadPostData();
            LoadUser();
            LoadLanguage();
        }

        public void LoadIP()
        {
            ip = Request.ServerVariables["REMOTE_ADDR"];
        }

        public void LoadTitle(string title)
        {
            if (subdomain != null)
            {
                title = subdomain + "." + title;
            }
            ViewBag.title = title;
        }

        public void LoadIds()
        {
            if (Request.RequestContext.RouteData.Values["id"] != null)
            {
                ids = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (string id in (string[])(Request.RequestContext.RouteData.Values["id"]))
                {
                    if (id.Contains("="))
                    {
                        string[] idParts = HttpUtility.UrlDecode(id).Split(new char[] { '=' }, 2);
                        if (!ids.ContainsKey(idParts[0]))
                        {
                            ids.Add(idParts[0], idParts[1]);
                        }
                        
                    }
                    else
                    {
                        ids.Add(id, null);
                    }
                }
                ViewBag.ids = ids;
            }
        }

        public void LoadPostData()
        {
            var  data = Request.Form.Keys;
            if (data.Count > 0)
            {
                postData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach(string key in data){
                    if (Request.Form[key] != null)
                    {
                        string value = Request.Form[key].ToString();
                        postData.Add(key, value);
                    }
                }
            }
        }

        public void LoadLanguage()
        {
            try
            {
                if (Request.Cookies["langName"] != null)
                {
                    this.lang = new Lang(Convert.ToString(Request.Cookies["langName"].Value));
                }
                else if (user != null)
                {
                    this.lang = new Lang(user.GetLang().GetLangID());
                }
                else
                {
                    this.lang = new Lang("English");
                }
            }
            catch
            {
                this.lang = new Lang("English");
            }
            ViewBag.lang = this.lang;
        }


        public void LoadUser()
        {
            
            if (Request.Cookies["userID"] == null)
            {
                return;
            }
            if (Request.Cookies["sessionToken"] == null)
            {
                return;
            }
            try
            {
                int userID = Convert.ToInt32(Request.Cookies["userID"].Value);
                string sessionToken = Convert.ToString(Request.Cookies["sessionToken"].Value);
                user = new User(userID);
                user.SignInWithSessionToken(sessionToken, Request.UserHostAddress);
            }
            catch(Exception e)
            {
                ViewBag.userError = e.Message;
                this.user = null;
            }

            if (this.user != null && this.user.GetSessionToken() == null)
            {
                this.user = null;
            }
            ViewBag.user = this.user;
        }

        public new ActionResult RedirectToAction(string actionName)
        {
            return RedirectToAction(actionName, subdomain);
        }

        public new ActionResult RedirectToAction(string actionName, string controllerName)
        {
            string protocol = "http://";
            if (Request.IsSecureConnection)
            {
                protocol = "https://";
            }
            if (controllerName.ToLower() == "main")
            {
                return Redirect(protocol + "bnsstore.com/" + actionName + "/");
            }
            return Redirect(protocol + controllerName + ".bnsstore.com/" + actionName + "/");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}