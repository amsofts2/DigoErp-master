using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Areas.Sales.Models;
using DigoErp.Resources.Sales;
using Microsoft.AspNet.Identity;

namespace DigoErp.Areas.Sales.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class InvoicesController : BaseController
    {
        private readonly InvoiceService invoiceService;
        private readonly CurrencyService currencyService;
        private readonly CategoryService categoryService;
        private readonly CustomersService customerService;
        private readonly ItemService itemService;
        private readonly TaxService taxService;
        private readonly DefaultService defaultService;
        private readonly BranchService branchService;
        private readonly EmailService emailService;

        public InvoicesController()
        {
            invoiceService = new InvoiceService();
            currencyService = new CurrencyService();
            categoryService = new CategoryService();
            customerService = new CustomersService();
            itemService = new ItemService();
            taxService = new TaxService();
            defaultService = new DefaultService();
            branchService = new BranchService();
            emailService = new EmailService();
        }
        // GET: Sales/Invoices
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            var response = invoiceService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            var viewModel = new InvoiceViewModel
            {
                Categories = categoryService.GetAll(),
                Currencies = currencyService.GetAll(),
                Customers = customerService.GetAll(),
                Items = itemService.GetAll(),
                Taxes = taxService.GetAll(),
                DefaultSetting = defaultService.GetByUserId(LogedInUser.Id),
                Invoice_Number = id > 0 ? string.Empty : invoiceService.GenerateInvoiceNumber(),
                Invoice = id > 0 ? invoiceService.GetById(id ?? 0) : new Invoice()
            };
            return View(viewModel);
        }

        public ActionResult Details(long? id)
        {
            var invoice = invoiceService.GetById(id ?? 0) ?? new Invoice();
            var viewModel = new InvoiceDetailViewModel
            {
                Invoice = invoice,
                Currency = currencyService.GetById(invoice.CurrencyId ?? 0) ?? new Currency(),
                Customer = customerService.GetById(invoice.CustomerId ?? 0) ?? new Customer(),
                Branch = branchService.GetById(LogedInUser.BranchId ?? 0)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetInvoiceById(int invoiceId)
        {
            var invoice = invoiceService.GetById(invoiceId);
            return Json(invoice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Invoice invoice)
        {
            try
            {
                var logoName = Path.GetFileName(invoice.file.FileName);
                var folderPath = Server.MapPath("~/images/invoices");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var path = Path.Combine(folderPath, logoName);
                invoice.file.SaveAs(path);
                invoice.Attachment = "images/invoices/" + logoName;
            }
            catch (Exception)
            {
            }

            try
            {
                invoice.InvoiceItems = JsonConvert.DeserializeObject<List<InvoiceItem>>(Request.Form["InvoiceItems"]);
                invoiceService.AddOrUpdate(invoice);

                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = invoice.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult GetByInvoiceNumber(string invoicenumber)
        {
            var item = invoiceService.GetByInvoiceNumber(invoicenumber);
            var response = new ResponseModel
            {
                StatusCode = (int)(item?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = item?.Id > 0 ? InvoiceRes.InvoiceNumberAlreadyExist : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long invoiceId)
        {
            try
            {
                invoiceService.Delete(invoiceId);
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

        public ActionResult UpdateStatus(long invoiceId,string status)
        {
            try
            {
                invoiceService.UpdateStatus(invoiceId,status);
                return Redirect("~/sales/invoices");
            }
            catch (Exception)
            {
                return Redirect("~/sales/invoices");
            }
        }

        public ActionResult SendEmail(long? customerId)
        {
            try
            {
                emailService.SendAsync(new IdentityMessage());
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}