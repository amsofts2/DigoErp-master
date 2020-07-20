using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using System.Net;
using System.Web.Mvc;
using DigoErp.Controllers;
using DigoErp.App_Start;

namespace DigoErp.Areas.Banking.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class AccountsController : BaseController
    {
        private readonly AccountService accountService;
        private readonly CurrencyService currencyService;
        public AccountsController()
        {
            accountService = new AccountService();
            currencyService = new CurrencyService();
        }

        // GET: Banking/Accounts
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            var response = accountService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            var viewModel = new AccountViewModel
            {
                Currencies = currencyService.GetAll(),
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetAccountById(int accountId)
        {
            var account = accountService.GetById(accountId);
            return Json(account, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Account account)
        {
            try
            {
                accountService.AddOrUpdateAccount(account);

                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = account.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);

            }
            catch (System.Exception)
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
        public ActionResult GetByAccountName(string accountname)
        {
            var item = accountService.GetByAccountName(accountname);
            var response = new ResponseModel
            {
                StatusCode = (int)(item?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = item?.Id > 0 ? AppResource.CategoryAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int accountId)
        {
            try
            {
                accountService.Delete(accountId);
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