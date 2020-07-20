using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;

namespace DigoErp.Areas.Settings.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class DefaultController : BaseController
    {
        private readonly DefaultService defaultService;
        private readonly AccountService accountService;
        private readonly CurrencyService currencyService;
        private readonly TaxService taxService;

        public DefaultController()
        {
            defaultService = new DefaultService();
            accountService = new AccountService();
            currencyService = new CurrencyService();
            taxService = new TaxService();
        }

        // GET: Settings/Default
        public ActionResult Index()
        {
            var viewModel = new DefaultViewModel
            {
                Accounts = accountService.GetAll(),
                Currencies = currencyService.GetAll(),
                Taxs = taxService.GetAll()
            };
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult GetDefaultSettings()
        {
            var defaultSettings = defaultService.GetByUserId(LogedInUser.Id);
            return Json(defaultSettings, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(Default @default)
        {
            try
            {
                var logoName = Path.GetFileName(@default.file.FileName);
                var folderPath = Server.MapPath("~/images/defaults");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string path = Path.Combine(folderPath, logoName);
                @default.file.SaveAs(path);
                @default.Logo = "images/defaults/" + logoName;
            }
            catch (System.Exception)
            {
            }

            try
            {
                if (ModelState.IsValid)
                {
                    @default.CreatedBy = LogedInUser.Id;
                    defaultService.AddOrUpdate(@default);

                    Session["_culture"] = @default.Language;

                    var responseModel = new ResponseModel
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        MessageAr = @default.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
                    };
                    return Json(responseModel, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var responseModel = new ResponseModel
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        MessageAr = AppResource.ChangesNotSaved
                    };
                    return Json(responseModel, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    MessageAr = AppResource.ChangesNotSaved
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
        }
    }
}