using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Areas.Purchases.Models;
using DigoErp.Areas.Sales.Models;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace DigoErp.Areas.Purchases.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class BillsController : BaseController
    {
        private readonly BillService billService;
        private readonly VendorService vendorService;
        private readonly CategoryService categoryService;
        private readonly CurrencyService currencyService;
        private readonly ItemService itemService;
        private readonly TaxService taxService;
        private readonly DefaultService defaultService;
        private readonly BranchService branchService;
        private readonly EmailService emailService;

        public BillsController()
        {
            billService = new BillService();
            vendorService = new VendorService();
            categoryService = new CategoryService();
            currencyService = new CurrencyService();
            itemService = new ItemService();
            taxService = new TaxService();
            defaultService = new DefaultService();
            branchService = new BranchService();
            emailService = new EmailService();
        }
        // GET: Purchase/Bills
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            var response = billService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(long? id)
        {
            var viewModel = new BillViewModel
            {
                Categories = categoryService.GetAll(),
                Currencies = currencyService.GetAll(),
                Vendors = vendorService.GetAll(),
                Items = itemService.GetAll(),
                Taxes = taxService.GetAll(),
                DefaultSetting = defaultService.GetByUserId(LogedInUser.Id),
                Bill = id > 0 ? billService.GetById((long)id) : new Bill
                {
                    Number = billService.GenerateBillNumber()
                }
            };
            return View(viewModel);
        }

        public ActionResult Details(long? id)
        {
            var bill = billService.GetById(id ?? 0) ?? new Bill();
            var viewModel = new BillDetailViewModel
            {
                Bill = bill,
                Currency = currencyService.GetById(bill.CurrencyId ?? 0) ?? new Currency(),
                Vendor = vendorService.GetById(bill.VendorId ?? 0) ?? new Vendor(),
                Branch = branchService.GetById(LogedInUser.BranchId ?? 0) 
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetBillById(int billId)
        {
            var item = billService.GetById(billId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Bill bill)
        {
            try
            {
                var logoName = Path.GetFileName(bill.file.FileName);
                var folderPath = Server.MapPath("~/images/bills");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var path = Path.Combine(folderPath, logoName);
                bill.file.SaveAs(path);
                bill.Attachment = "/images/bills/" + logoName;
            }
            catch (System.Exception)
            {
            }

            try
            {
                bill.Bill_Items = JsonConvert.DeserializeObject<List<Bill_Item>>(Request.Form["Bill_Items"]);
                billService.AddOrUpdate(bill);

                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = bill.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult GetByBillNumber(string number)
        {
            var item = billService.GetByBillNumber(number);
            var response = new ResponseModel
            {
                StatusCode = (int)(item?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = item?.Id > 0 ? AppResource.CategoryAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int billId)
        {
            try
            {
                billService.Delete(billId);
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

        public ActionResult UpdateStatus(long bill_Id, string status)
        {
            try
            {
                billService.UpdateStatus(bill_Id, status);
                return Redirect("~/purchases/bills");
            }
            catch (Exception)
            {
                return Redirect("~/purchases/bills");
            }
        }

        public ActionResult SendEmail(long? vendorid)
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