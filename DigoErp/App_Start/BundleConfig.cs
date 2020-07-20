using System.Web.Optimization;

namespace DigoErp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                        "~/assets/js/app.min.js",
                        "~/assets/js/theme/default.min.js",
                        "~/assets/plugins/d3/d3.min.js",
                        "~/assets/plugins/nvd3/nv.d3.js",
                        "~/assets/plugins/jvectormap-next/jquery-jvectormap.min.js",
                        "~/assets/plugins/jvectormap-next/jquery-jvectormap-world-mill.js",
                        "~/assets/plugins/apexcharts/apexcharts.min.js",
                        "~/assets/plugins/moment/moment.js",
                        "~/assets/plugins/bootstrap-daterangepicker/daterangepicker.js",
                        "~/assets/plugins/datatables.net/js/jquery.dataTables.min.js",
                        "~/assets/plugins/datatables.net-bs4/js/dataTables.bootstrap4.min.js",
                        "~/assets/plugins/datatables.net-responsive/js/dataTables.responsive.min.js",
                        "~/assets/plugins/datatables.net-responsive-bs4/js/responsive.bootstrap4.min.js",
                        "~/Scripts/bootstrap-colorpicker.js",
                        "~/Scripts/bootstrap-datepicker.min.js",
                        "~/assets/plugins/bootstrap-select/js/bootstrap-select.min.js",
                        "~/assets/plugins/sweetalert/sweetalert.min.js",
                        "~/assets/plugins/parsley/parsley.js"

                        ));

            bundles.Add(new ScriptBundle("~/Scripts/angularjs").Include(
                        "~/assets/plugins/angular/angular.min.js",
                        "~/assets/plugins/angular/angular-route.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Scripts/arabicJs").Include(
                       "~/assets/plugins/bootstrap-select/js/i18n/defaults-ar_AR.min.js",
                       "~/assets/plugins/parsley/i18n/ar.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/assets/requiredCss").Include(
                      "~/assets/css/default/app.min.css",
                      "~/assets/plugins/jvectormap-next/jquery-jvectormap.css",
                      "~/assets/plugins/nvd3/nv.d3.css",
                      "~/assets/plugins/bootstrap-daterangepicker/daterangepicker.css",
                      "~/assets/plugins/flag-icon-css/css/flag-icon.min.css",
                      "~/assets/plugins/datatables.net-bs4/css/dataTables.bootstrap4.min.css",
                      "~/assets/plugins/datatables.net-responsive-bs4/css/responsive.bootstrap4.min.css",
                      "~/assets/plugins/bootstrap-datepicker/css/bootstrap-datepicker.css",
                      "~/assets/css/bootstrap-colorpicker.css",
                      "~/assets/plugins/bootstrap-select/css/bootstrap-select.min.css",
                      "~/Content/Site.css"
                      ));
        }
    }
}
