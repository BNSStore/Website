using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.App_Start
{
    public class Logger
    {
        private HttpContextBase httpContext;

        public Logger(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }


    }
}