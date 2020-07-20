using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace DigoErp.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            controller = controller?.ToLower();
            action = action?.ToLower();

            currentAction = currentAction?.ToLower();
            currentController = currentController?.ToLower();

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            if (controller == currentController && action == currentAction)
            {
                return "active";
            }
            return controller == currentController ? "active" : string.Empty;
        }

        public static string IsMenuSelected(this HtmlHelper html, string controller = null)
        {
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];
            currentController = currentController?.ToLower();
            return controller == currentController ? "active" : string.Empty;
        }
        public static string IsMenuSelected(this HtmlHelper html, params string[] controllers)
        {
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];
            currentController = currentController?.ToLower();
            return controllers.Any(c => c == currentController) ? "active" : string.Empty;
        }
        public static string PageClass(this HtmlHelper html)
        {
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
        public static IHtmlString InlineScripts(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, htmlTagName: "script");
        }

        public static IHtmlString InlineStyles(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, htmlTagName: "style");
        }

        private static IHtmlString InlineBundle(this HtmlHelper htmlHelper, string bundleVirtualPath, string htmlTagName)
        {
            var bundleContent = LoadBundleContent(htmlHelper.ViewContext.HttpContext, bundleVirtualPath);
            var htmlTag = string.Format("<{0}>{1}</{0}>", htmlTagName, bundleContent);
            return new HtmlString(htmlTag);
        }

        private static string LoadBundleContent(HttpContextBase httpContext, string bundleVirtualPath)
        {
            var bundleContext = new BundleContext(httpContext, BundleTable.Bundles, bundleVirtualPath);
            var bundle = BundleTable.Bundles.Single(b => b.Path == bundleVirtualPath);
            var bundleResponse = bundle.GenerateBundleResponse(bundleContext);
            return bundleResponse.Content;
        }
    }
}