using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using System.Net;
using System.Web.Mvc;
using DigoErp.Controllers;
using DigoErp.App_Start;

namespace DigoErp.Areas.Sales.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class CustomersController : BaseController
    {
        private readonly CustomersService customerService;
        private readonly CurrencyService currencyService;
        public CustomersController()
        {
            customerService = new CustomersService();
            currencyService = new CurrencyService();
        }
        // GET: Sales/Customers
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Customer> response = customerService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            var viewModel = new CustomerViewModel
            {
                Currencies = currencyService.GetAll(),
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetCustomerById(int customerId)
        {
            var customer = customerService.GetById(customerId);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Customer customers)
        {
            try
            {
                {
                    customerService.AddOrUpdate(customers);
                    var responseModel = new ResponseModel
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        MessageAr = customers.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
                    };
                    return Json(responseModel, JsonRequestBehavior.AllowGet);
                }
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
            var customer = customerService.GetByEmail(email);
            ResponseModel response = new ResponseModel
            {
                StatusCode = (int)(customer?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = customer?.Id > 0 ? AppResource.EmailAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int customerId)
        {
            try
            {
                customerService.Delete(customerId);
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