using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Controllers;

namespace DigoErp.Areas.Settings.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class SettingsController : BaseController
    {
        // GET: Settings/Settings
        public ActionResult Index()
        {
            return View();
        }
    }
}