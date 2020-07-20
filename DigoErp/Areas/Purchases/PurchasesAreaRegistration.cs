using System.Web.Mvc;

namespace DigoErp.Areas.Purchases
{
    public class PurchaseAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "purchases";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.AreaName.ToLower();
            context.MapRoute(
                "purchases_default",
                "purchases/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}