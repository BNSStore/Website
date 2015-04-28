using Microsoft.AspNet.SignalR;
using SLouple.MVC.App_Start;
using SLouple.MVC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SLouple
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(6);

            QuartzStartUp.StartUp();
        }
    }
}
