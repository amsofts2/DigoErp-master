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

namespace DigoErp.Areas.Purchases.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class PaymentsController : BaseController
    {
        private readonly PaymentService paymentService;
        private readonly BillService billService;
        private readonly VendorService vendorService;
        private readonly CategoryService categoryService;
        private readonly AccountService accountService;

        public PaymentsController()
        {
            paymentService = new PaymentService();
            billService = new BillService();
            vendorService = new VendorService();
            categoryService = new CategoryService();
            accountService = new AccountService();
        }
        // GET: Purchases/Payments
        public ActionResult Index()
        {
            return View();
        }
         [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Payment> response = paymentService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            PaymentViewModel viewModel = new PaymentViewModel
            {

                vendors = vendorService.GetAll(),
                Accounts = accountService.GetAll(),
                Categories = categoryService.GetAll(),
                Bills = billService.GetAll(),
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetPaymentById(int paymentId)
        {
            var payment = paymentService.GetById(paymentId);
            return Json(payment, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Payment payment)
        {
            try
            {
                string logoName = Path.GetFileName(payment.file.FileName);
                var folderPath = Server.MapPath("~/images/payments");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string path = Path.Combine(folderPath, logoName);
                payment.file.SaveAs(path);
                payment.Attachment = "images/payments/" + logoName;
            }
            catch (System.Exception)
            {
            }

            try
            {
                    paymentService.AddOrUpdatePayment(payment);
                    var responseModel = new ResponseModel
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        MessageAr = payment.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult Delete(int paymentId)
        {
            try
            {
                paymentService.Delete(paymentId);
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