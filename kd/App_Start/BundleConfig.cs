using System.Web;
using System.Web.Optimization;

namespace kd
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js", 
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.wizard.js",                        
                        "~/Scripts/jquery.ui.custom.js",
                        "~/Scripts/jquery.flot.min.js",
                        "~/Scripts/jquery.flot.pie.min.js",
                        "~/Scripts/jquery.flot.resize.min.js",
                        "~/Scripts/fullcalendar.min.js",
                        "~/Scripts/jquery.gritter.min.js",
                        "~/Scripts/matrix.chat.js",
                        "~/Scripts/jquery.uniform.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-responsive.min.css",
                      "~/Content/fullcalendar.css",
                      "~/Content/matrix-style.css",
                      "~/Content/matrix-media.css",
                      "~/font/font-awesome.css",
                      "~/Content/jquery.gritter.css"));
        }
    }
}
