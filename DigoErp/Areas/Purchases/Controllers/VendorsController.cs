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
    public class VendorsController : BaseController
    {
        private readonly VendorService vendorService;
        private readonly CurrencyService currencyService;

        public VendorsController()
        {
            vendorService = new VendorService();
            currencyService = new CurrencyService();
        }
        // GET: Purchases/Vendors
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            var response = vendorService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            var viewModel = new VendorViewModel
            {
                Currencies = currencyService.GetAll(),
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetVendorById(int vendorId)
        {
            var item = vendorService.GetById(vendorId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Vendor vendor)
        {
            try
            {
                var logoName = Path.GetFileName(vendor.file.FileName);
                var folderPath = Server.MapPath("~/images/vendors");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var path = Path.Combine(folderPath, logoName);
                vendor.file.SaveAs(path);
                vendor.Photo = "images/vendors/" + logoName;
            }
            catch (System.Exception)
            {
            }
            try
            {
                    vendorService.AddOrUpdate(vendor);
                    var responseModel = new ResponseModel
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        MessageAr = vendor.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult GetByEmail(string email)
        {
            var user = vendorService.GetByEmail(email);
            var response = new ResponseModel
            {
                StatusCode = (int)(user?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = user?.Id > 0 ? AppResource.EmailAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Delete(int vendorId)
        {
            try
            {
                vendorService.Delete(vendorId);
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