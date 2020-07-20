using System.Net;
using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Resources.Settings.Taxes;
using DigoErp.Service.Models;
using DigoErp.Service.Services;

namespace DigoErp.Areas.Settings.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class CurrenciesController : BaseController
    {
        private readonly CurrencyService currencyService;

        public CurrenciesController()
        {
          currencyService = new CurrencyService();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Currency> response = currencyService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Currency currency)
        {
            try
            {
                currencyService.AddOrUpdate(currency);
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = currency.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    MessageAr = AppResource.ChangesNotSaved
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetById(int currencyId)
        {
            var item = currencyService.GetById(currencyId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetByName(string name)
        {
            var tax = currencyService.GetByName(name);
            var response = new ResponseModel
            {
                StatusCode = (int)(tax?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = tax?.Id > 0 ? TaxRes.TaxAlreadyExist : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int currencyId)
        {
            try
            {
                currencyService.Delete(currencyId);
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = AppResource.DeletedSuccessfully
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = AppResource.DeleteErrorValidation
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
        }
    }
}