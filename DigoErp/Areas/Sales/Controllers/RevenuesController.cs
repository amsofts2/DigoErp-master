using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace DigoErp.Areas.Sales.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class RevenuesController : BaseController
    {
        private readonly RevenueService revenueService;
        private readonly AccountService accountService;
        private readonly CategoryService categoryService;
        private readonly CustomersService customerService;
        public RevenuesController()
        {
            revenueService = new RevenueService();
            accountService = new AccountService();
            categoryService = new CategoryService();
            customerService = new CustomersService();
        }
        // GET: Sales/Revenues
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Revenue> response = revenueService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            RevenueViewModel viewModel = new RevenueViewModel
            {
                Accounts = accountService.GetAll(),
                Categories = categoryService.GetAll(),
                Customers = customerService.GetAll(),
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetRevenueById(int revenueId)
        {
            var item = revenueService.GetById(revenueId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Revenue revenue)
        {
            try
            {
                string logoName = Path.GetFileName(revenue.file.FileName);
                var folderPath = Server.MapPath("~/images/revenues");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string path = Path.Combine(folderPath, logoName);
                revenue.file.SaveAs(path);
                revenue.Attachment = "/images/revenues/" + logoName;
            }
            catch (System.Exception)
            {
            }

            try
            {
                revenueService.AddOrUpdate(revenue);

                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = revenue.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult Delete(int revenueId)
        {
            try
            {
                revenueService.Delete(revenueId);
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