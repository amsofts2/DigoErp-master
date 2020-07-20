using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Enums;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net;
using System.Web.Mvc;

namespace DigoErp.Areas.Banking.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class TransfersController : BaseController
    {
        private readonly TransferService transferService;
        private readonly AccountService accountService;

        public TransfersController()
        {
            transferService = new TransferService();
            accountService = new AccountService();
        }
        // GET: Banking/Transfers
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Transfer> response = transferService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            TransferViewModel viewModel = new TransferViewModel
            {
                Accounts = accountService.GetAll(),
                Id = id ?? 0
            };
            ViewData["PaymentMethod"] = JsonConvert.SerializeObject(Enum.GetValues(typeof(PaymentMethod)), Formatting.Indented, new StringEnumConverter());
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetTransferById(int transferId)
        {
            var account = transferService.GetById(transferId);
            return Json(account, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Transfer transfer)
        {
            try
            {
                transferService.AddOrUpdate(transfer);

                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = transfer.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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

        [HttpPost]
        public ActionResult Delete(int transferId)
        {
            try
            {
                transferService.Delete(transferId);
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