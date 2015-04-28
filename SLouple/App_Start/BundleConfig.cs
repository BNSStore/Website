using System.Web;
using System.Web.Optimization;

namespace SLouple.MVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/example/example").Include(
                      "~/example/example.js",
                      "~/example/example.js"));
        }
    }
}