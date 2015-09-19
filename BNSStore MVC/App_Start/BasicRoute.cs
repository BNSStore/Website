using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace BNSStore.MVC.App_Start
{
    public class BasicRoute : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.Headers["Origin"] != null)
            {
                httpContext.Response.AppendHeader("Access-Control-Allow-Origin", httpContext.Request.Headers["Origin"]);
                httpContext.Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            }

            string url = httpContext.Request.Url.Host + httpContext.Request.Url.PathAndQuery;

            //Remove / at the end of URL
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            string[] urlParts = url.Split('/');

            //Get SubDomain
            string[] domainParts = urlParts[0].Split('.');
            string subDomain;
            if (domainParts.Length > 2)
            {
                subDomain = domainParts[0];
            }
            else
            {
                subDomain = "Main";
            }

            //Capitalize first letter
            subDomain = char.ToUpper(subDomain[0]) + subDomain.ToLower().Substring(1);

            //Decide which route method
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

            if (urlParts[urlParts.Length - 1].Contains("."))
            {
                filePath = String.Join("/", urlParts.Skip(filePathIndex).Take(urlParts.Length - 2).ToArray());
                routeData.Values.Add("file", "/Resources/" + subDomain + "/" + filePath);
            }
            else
            {
                for (int i = filePathIndex; i < urlParts.Length - 1; i++)
                {
                    filePath += urlParts[i] + "/";
                }
                filePath = filePath.Substring(0, filePath.Length - 1);

                fileExtension = urlParts[urlParts.Length - 1];
                routeData.Values.Add("file", "/Resources/" + subDomain + "/" + filePath + "." + fileExtension);
            }
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

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}