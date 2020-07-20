using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;

namespace DigoErp.Areas.Banking.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class ReconciliationsController : BaseController
    {
        private readonly ReconciliationService reconciliationService;
        private readonly AccountService accountService;

        public ReconciliationsController()
        {
            reconciliationService = new ReconciliationService();
            accountService = new AccountService();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            searchModel.Created_By = LogedInUser.Id;
            var response = reconciliationService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            List<Account> accounts = accountService.GetAll();
            return View(accounts);
        }


        [HttpPost]
        public ActionResult AddOrUpdate(Reconciliation reconciliation)
        {
            try
            {
                reconciliation.Created_By = LogedInUser.Id;
                reconciliationService.AddOrUpdate(reconciliation);

                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = reconciliation.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public ActionResult GetById(long id)
        {
            var reconciliation = reconciliationService.GetById(id);
            return Json(reconciliation, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(long? id)
        {
            List<Account> accounts = accountService.GetAll();
            ViewBag.Id = id ?? 0;
            return View(accounts);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                reconciliationService.Delete(id);
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = AppResource.DeletedSuccessfully
                };
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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