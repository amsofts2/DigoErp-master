using System;
using System.Web.Mvc;
using DigoErp.Helpers;
using DigoErp.Service.Models;
using DigoErp.Service.Services;

namespace DigoErp.Controllers
{
    public class SetCultureController : Controller
    {
        private readonly UserService userService;
        public SetCultureController()
        {
            userService  = new UserService();
        }
        public ActionResult SetCulture(string culture)
        {
            culture = CultureHelper.GetImplementedCulture(culture);
            // Validate input
            Session["_culture"] = culture;
            var urlRef = Request.UrlReferrer?.ToString();
            try
            {
                var userdetail = Session["UserDetail"] as User ?? new User();
                userdetail.Language = culture;
                userService.AddOrUpdate(userdetail);
            }
            catch(Exception)
            {

            }
            if (string.IsNullOrEmpty(urlRef))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(urlRef);
        }
    }
}