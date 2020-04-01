using System.Web.Optimization;

namespace VeronaTour.WEB
{
    /// <summary>
    ///     Configure bundling, that combines multiple files into a single file.
    ///     Fewer files means fewer HTTP requests and that can improve first page load performance.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        ///     Registers bundles into existing application bundle collection
        /// </summary>
        /// <param name="bundles">Bundle collection</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles
                .Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles
                .Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles
                .Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles
                .Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js"));

            bundles
                .Add(new ScriptBundle("~/bundles/scripts")
                .Include(
                    "~/Scripts/jquery.maskedinput.min.js",
                    "~/Scripts/umd/popper.min.js",
                    "~/Scripts/script/easy-responsive-tabs.js",
                    "~/Scripts/script/owl.carousel.js",
                    "~/Scripts/script/script.js"));

            bundles
                .Add(new ScriptBundle("~/bundles/AdminLTE")
                .Include(
                    "~/Content/AdminLTE/bower_components/jquery/dist/jquery.min.js",
                    "~/Content/AdminLTE/bower_components/bootstrap/dist/js/bootstrap.min.js",
                    "~/Content/AdminLTE/bower_components/select2/dist/js/select2.full.js",
                    "~/Content/AdminLTE/dist/js/adminlte.min.js",
                    "~/Content/AdminLTE/bower_components/ckeditor/ckeditor.js",
                    "~/Content/AdminLTE/bower_components/ckeditor/adapters/jquery.js"));

            bundles
              .Add(new StyleBundle("~/Content/AdminLTE")
              .Include(
                  "~/Content/AdminLTE/bower_components/bootstrap/dist/css/bootstrap.min.css",
                  "~/Content/AdminLTE/bower_components/select2/dist/css/select2.min.css",
                  "~/Content/AdminLTE/bower_components/font-awesome/css/font-awesome.min.css",
                  "~/Content/AdminLTE/bower_components/Ionicons/css/ionicons.min.css",
                  "~/Content/AdminLTE/dist/css/AdminLTE.min.css",
                  "~/Content/AdminLTE/dist/css/skins/_all-skins.min.css",
                  "~/Content/AdminLTE/style.css"));

            bundles
                .Add(new StyleBundle("~/Content/css")
                .Include(
                    "~/Content/bootstrap.css",
                    "~/Content/style-adaptability.css",
                    "~/Content/css/style.css",
                    "~/Content/css/owl.carousel.css",
                    "~/Content/css/owl.theme.css",
                    "~/Content/css/shop.css",
                    "~/Content/css/easy-responsive-tabs.css"));
            }
    }
}
