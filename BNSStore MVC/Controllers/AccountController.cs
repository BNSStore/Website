using SLouple.MVC.Account;
using SLouple.MVC.Shared;
using System;
using System.Web;
using System.Web.Mvc;

namespace BNSStore.MVC.Controllers
{
    public class AccountController : AdvancedController
    {
        public AccountController()
        {
            subdomain = "Account";
        }

        public ActionResult Default()
        {
            return Login();
        }

        public ActionResult Settings()
        {
            string title = "Settings";
            Load(title);
            if (user != null)
            {
                if (postData != null && postData.ContainsKey("method"))
                {
                    try
                    {
                        string method = postData["method"];
                        switch (method)
                        {
                            case "emailSub": EmailSub(); break;
                            default: ; break;
                        }
                        return Content("success");
                    }
                    catch
                    {
                        return Content("failed");
                    }
                }
                return View(title);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        #region Settings Methods

        private void EmailSub()
        {

        }

        #endregion 

        public ActionResult Login()
        {
            string title = "Login";
            Load(title);
            bool simple = ids != null && ids.ContainsKey("simple") && Convert.ToBoolean(ids["simple"]) == true;
            if (user != null)
            {
                return RedirectToAction("Settings");
            }
            if (postData != null)
            {
                try
                {
                    string username = postData["username"];
                    string password = postData["password"];
                    user = new User(username);
                    user.SignInWithPassword(password, ip);
                    if (user.GetSessionToken() != null)
                    {
                        if (simple)
                        {
                            return Content(user.GetUserID() + "|" + user.GetSessionToken());
                        }
                        HttpCookie userIDCookie = new HttpCookie("userID", Convert.ToString(user.GetUserID()));
                        userIDCookie.Path = "/";
                        userIDCookie.Domain = ".bnsstore.com";
                        HttpCookie sessionTokenCookie = new HttpCookie("sessionToken", Convert.ToString(user.GetSessionToken()));
                        sessionTokenCookie.Path = "/";
                        sessionTokenCookie.Domain = ".bnsstore.com";

                        Response.Cookies.Add(userIDCookie);
                        Response.Cookies.Add(sessionTokenCookie);

                        if (ids != null && ids.ContainsKey("preURL"))
                        {
                            return Redirect(ids["preURL"]);
                        }
                        else
                        {
                            return RedirectToAction("Settings");
                        }
                    }
                    else
                    {
                        if (simple)
                        {
                            return Content("false");
                        }
                        else
                        {
                            this.user = null;
                            ViewBag.LoginFailed = true;
                        }
                    }
                }
                catch
                {
                    if (simple)
                    {
                        return Content("false");
                    }
                    else
                    {
                        this.user = null;
                        ViewBag.LoginFailed = true;
                    }
                }
            }
            if (simple)
            {
                return Content("");
            }
            return View(title);
        }

        public ActionResult SignUp()
        {
            return Redirect("//account.slouple.com/SignUp/");
        }
	}
}