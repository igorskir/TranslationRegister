using System.Web;
using System.Web.Optimization;

namespace TranslationReg
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.unobtrusive*",
                        "~/Scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                      "~/Scripts/modernizr/modernizr-2.6.2.js"));

            bundles.Add(new StyleBundle("~/Style/css").Include(
                      "~/Style/bootstrap/bootstrap.css",
                      "~/Style/style.css")
                      .IncludeDirectory("~/Style/buttons", "*.css")
                      .IncludeDirectory("~/Style/cards", "*.css")
                      .IncludeDirectory("~/Style/modal", "*.css")
                      .IncludeDirectory("~/Style/search", "*.css")
                      .IncludeDirectory("~/Style/tables", "*.css")
                      .IncludeDirectory("~/Style/icons", "*.css")
                      );
        }
    }
}
