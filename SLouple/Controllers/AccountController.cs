using SLouple.MVC.Account;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLouple.MVC.Controllers
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
            bool sub = Convert.ToBoolean(postData["sub"]);
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            if (sub)
            {
                sqlSP.UserAddEmailSub(user.userID);
            }
            else
            {
                sqlSP.UserDelEmailSub(user.userID);
            }
        }

        #endregion

        public ActionResult SignUpCheck()
        {
            LoadIds();
                string checkName = ids["checkName"];
                switch (checkName)
                {
                    case "emailExist":
                        {
                            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                            return Content(Convert.ToString(sqlSP.UserEmailExist(ids["email"])));
                        }
                    case "usernameExist":
                        {
                            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                            return Content(Convert.ToString(sqlSP.UserUsernameExist(ids["username"])));
                        }
                    default:
                        {
                            return Content("null");
                        }
                }
        }

        public ActionResult VerifyEmail()
        {
            string title = "VerifyEmail";
            Load(title);
            if (ids != null)
            {
                    SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                    ViewBag.verified = sqlSP.UserVerifyEmail(ids["emailAddress"], ids["verifyString"]);
                    return View(title);
                
            }
            return RedirectToAction("SignUp");
            
        }

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
                    user = new User(-1, username, password, ip, null);
                    if (user.sessionToken != null)
                    {
                        if (simple)
                        {
                            return Content(user.userID + "|" + user.sessionToken);
                        }
                        HttpCookie userIDCookie = new HttpCookie("userID", Convert.ToString(user.userID));
                        userIDCookie.Path = "/";
                        userIDCookie.Domain = ".bnsstore.com";
                        HttpCookie sessionTokenCookie = new HttpCookie("sessionToken", Convert.ToString(user.sessionToken));
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
            string title = "SignUp";
            Load(title);
            if (user != null)
            {
                return RedirectToAction("Settings");
            }
            if (postData != null)
            {
                bool simple = ids != null && ids.ContainsKey("simple") && Convert.ToBoolean(ids["simple"]) == true;
                try
                {
                    string username = postData["username"];
                    string displayName = postData["displayName"];
                    string password = postData["password"];
                    string emailAddress = postData["emailAddress"];
                    string reCapResponse = postData["g-recaptcha-response"];
                    int userID = SLouple.MVC.Account.User.CreateUser(username, displayName, password, emailAddress, lang.langName, ip, reCapResponse);
                    if (userID > 0)
                    {
                        if (simple)
                        {
                            return Content("success");
                        }

                        HttpCookie userIDCookie = new HttpCookie("userID", Convert.ToString(userID));
                        userIDCookie.Path = "/";
                        userIDCookie.Domain = ".bnsstore.com";
                        userIDCookie.Expires = new DateTime(2099, 12, 31);

                        string sessionToken = SLouple.MVC.Account.User.Login(userID, password, ip, null);
                        HttpCookie sessionTokenCookie = new HttpCookie("sessionToken", Convert.ToString(sessionToken));
                        sessionTokenCookie.Path = "/";
                        sessionTokenCookie.Domain = ".bnsstore.com";
                        sessionTokenCookie.Expires = new DateTime(2099, 12, 31);

                        HttpCookie firstLoadCookie = new HttpCookie("firstLoad", "true");
                        firstLoadCookie.Path = "/";
                        firstLoadCookie.Domain = ".bnsstore.com";
                        firstLoadCookie.Expires = DateTime.MinValue;

                        HttpCookie langNameCookie = new HttpCookie("langName");
                        langNameCookie.Domain = ".bnsstore.com";
                        langNameCookie.Path = "/";
                        langNameCookie.Expires = DateTime.Now.AddDays(-1d);

                        Request.Cookies.Add(userIDCookie);
                        Request.Cookies.Add(sessionTokenCookie);
                        Request.Cookies.Add(firstLoadCookie);
                        Request.Cookies.Add(langNameCookie);

                        Response.Cookies.Add(userIDCookie);
                        Response.Cookies.Add(sessionTokenCookie);
                        Response.Cookies.Add(firstLoadCookie);
                        Response.Cookies.Add(langNameCookie);

                        LoadUser();
                        return RedirectToAction("Settings");
                    }
                    else
                    {
                        if (simple)
                        {
                            return Content("false");
                        }
                        else
                        {
                            ViewBag.createUserFailed = true;
                        }
                    }
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    if (simple)
                    {
                        return Content("false");
                    }
                    else
                    {
                        ViewBag.createUserFailed = true;
                    }
                }
            }
            return View(title);
        }
	}
}