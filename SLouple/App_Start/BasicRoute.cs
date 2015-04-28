using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SLouple.MVC.App_Start
{
    public class BasicRoute : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if(httpContext.Request.Headers["Origin"] != null){
                httpContext.Response.AddHeader("Access-Control-Allow-Origin", httpContext.Request.Headers["Origin"]);
                httpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            }
            string url = httpContext.Request.Url.Host + httpContext.Request.Url.PathAndQuery;

            //Remove / at the end of URL
            if (url.EndsWith("/"))
            {
                url = url.Substring(0,url.Length - 1);
            }
            string[] urlParts = url.Split('/');

            
            string[] domainParts = urlParts[0].Split('.');

            //Support for localhost while debugging
            int subDomainLevel = 3;
            if (httpContext.Request.Url.Host.ToLower().EndsWith("localhost"))
            {
                subDomainLevel = 2;
            }else if (httpContext.Request.Url.Host.ToLower().EndsWith("bnsstore-001-site1.mywindowshosting.com"))
            {
                subDomainLevel = 4;
            }

            //Get SubDomain
            string subDomain;
            if (domainParts.Length >= subDomainLevel)
            {
                subDomain = domainParts[domainParts.Length - subDomainLevel];
                if (subDomain == "www")
                {
                    subDomain = "main";
                }
            }
            else
            {
                subDomain = "main";
            }

            

            //Capitalize first letter
            subDomain = char.ToUpper(subDomain[0]) + subDomain.ToLower().Substring(1);

            //Decide which route method
            if (httpContext.Request.Url.Host.ToLower().EndsWith("bnsstore-001-site1.mywindowshosting.com") && urlParts.Length >= 6 && (urlParts[2].ToLower() == "res" || urlParts[2].ToLower() == "resources"))
            {
                return ResouecesRoute(urlParts.Skip(1).ToArray(), urlParts[1]);
            }
            if (urlParts.Length >= 5 && (urlParts[1].ToLower() == "res" || urlParts[1].ToLower() == "resources"))
            {
                    return ResouecesRoute(urlParts, subDomain);
            }
            else
            {
                return DefaultRoute(urlParts, subDomain);
            }
        }

        private RouteData DefaultRoute(string[] urlParts, string subDomain)
        {
            RouteData routeData = new RouteData(this, new MvcRouteHandler());
            //Route Controller Default: Main
            routeData.Values.Add("controller", subDomain);
            //Route Action Default: Default
            if (urlParts.Length >= 2)
            {
                routeData.Values.Add("action", urlParts[1]);
            }
            else
            {
                routeData.Values.Add("action", "Default");
            }
            //Route ID
            if (urlParts.Length >= 3)
            {
                string[] ids = new string[urlParts.Length - 2];
                Array.Copy(urlParts, 2, ids, 0, urlParts.Length - 2);
                routeData.Values.Add("id", ids);
            }
            else
            {
                routeData.Values.Add("id", null);
            }
            return routeData;
        }

        private RouteData ResouecesRoute(string[] urlParts, string subDomain)
        {
            RouteData routeData = new RouteData(this, new MvcRouteHandler());
            routeData.Values.Add("controller", "Resources");
            //Action
            string action;
            int filePathIndex = 3;
            action = urlParts[2];
            if (!IsResourcesActionValid(action))
            {
                action = "default";
                filePathIndex--;
            }
            routeData.Values.Add("action", action);
            //File
            string filePath = "";
            string fileExtension;

            for (int i = filePathIndex; i < urlParts.Length - 1; i++)
            {
                filePath += urlParts[i] + "/";
            }
            filePath = filePath.Substring(0, filePath.Length - 1);

            fileExtension = urlParts[urlParts.Length - 1];
            if (!IsFileExtendsionValid(fileExtension))
            {
                //TODO: Change to 403
                return null;
            }
            routeData.Values.Add("file", "/Resources/" + subDomain + "/" + filePath + "." + fileExtension);
            return routeData;
        }

        private bool IsResourcesActionValid(string action)
        {
            action = action.ToLower();
            if (action == "default")
            {
                return true;
            }
            if (action == "download")
            {
                return true;
            }
            if (action == "get")
            {
                return true;
            }
            return false;
        }

        private bool IsFileExtendsionValid(string fileExtension)
        {
            fileExtension = fileExtension.ToLower();
            if (fileExtension == "config")
            {
                return false;
            }
            return true;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}