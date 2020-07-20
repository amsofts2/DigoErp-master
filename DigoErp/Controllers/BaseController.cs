using System;
using System.Threading;
using System.Web.Mvc;
using DigoErp.Helpers;
using DigoErp.Service.Models;

namespace DigoErp.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
            // Attempt to read the culture from session
            var culture = Session["_culture"];
            if (culture != null)
                cultureName = culture.ToString();
            //else
            //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
        protected string CustomDateFormat
        {
            get
            {
                return "dd-MM-yyyy";
            }
        }

        protected User LogedInUser
        {
            get
            {
                var userDetail = Session["UserDetail"] as User ?? new User();
                return userDetail;
            }
        }
    }
}