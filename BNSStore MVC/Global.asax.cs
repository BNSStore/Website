using Microsoft.AspNet.SignalR;
using BNSStore.MVC.App_Start;
using BNSStore.MVC.Shared;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using SLouple.MVC.Shared;

namespace BNSStore
{
    public class MVCApp : System.Web.HttpApplication
    {
        public static readonly Provider CurrentProvider = new Provider("BNSStore");

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(6);
            QuartzStartUp.StartUp();
            //Logger.StartWriting();
        }

        protected void Application_End()
        {
            //Logger.StopWriting();
        }

        protected void Application_BeginRequest()
        {
            //Context.Response.Filter = new ResponseLengthCalculatingStream(Context.Response.Filter);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            /*
            Logger logger = new Logger(this.Context, "ncsa");
            logger.Log();
             */
        }
    }
}
