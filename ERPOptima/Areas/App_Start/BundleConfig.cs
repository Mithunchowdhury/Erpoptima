using System.Web;
using System.Web.Optimization;

namespace Optima
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/underscore.js",
                        "~/Scripts/ng-table*",
                        "~/Scripts/Sales/app.js",
                        "~/Scripts/Sales/directive.js",
                        "~/Scripts/Sales/filter.js",
                        "~/Scripts/Sales/service.js",
                        "~/Scripts/services/crudservice.js",
                        "~/Scripts/plugins/Select2/select2.angular.js",
                        "~/Scripts/validation.js",
                         "~/Scripts/validationrule.js"
                        ));
         
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/tooltip.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/AdminLTE/script.js",
                        "~/Scripts/Helper/Helper.js",
                        "~/Scripts/plugins/Select2/select2.js",
                         "~/Scripts/plugins/datetimepicker/jquery.datetimepicker.js",                         
                        "~/Scripts/md5.js"
                        //, "~/Scripts/AdminLTE/demo.js"
                         ));
 
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/themes").Include(
                        "~/Content/themes/lte/css/bootstrap.css",
                        "~/Content/themes/lte/css/font-awesome.css",
                        "~/Content/themes/lte/css/ionicons.css",
                        "~/Content/themes/lte/css/Select2/select2.css",
                        "~/Content/datetimepicker/jquery.datetimepicker.css",
                         "~/Content/themes/lte/css/rwd-table.min.css",                       
                        "~/Content/themes/lte/css/toastr.css"                        
                        ));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/themes/lte/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));


            //comment
        }
    }
}