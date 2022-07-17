using System.Web;
using System.Web.Optimization;

namespace MVCApartmants
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content").Include(
                "~/Content/bootstrap.min.css", 
                "~/Content/jquery.fancybox.css", 
                "~/Content/jquery-ui.css", 
                "~/Content/jquery-ui.theme.css", 
                "~/Content/font-awsome.css",
                "~/Content/Site.css"
            ));

            bundles.Add(new Bundle("~/Scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js", 
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jqueryval"
            ));
        }
    }
}
