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
using DigoErp.Controllers;
using DigoErp.App_Start;

namespace DigoErp.Areas.Settings.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class CategoriesController : BaseController
    {
        private readonly CategoryService categoryService;

        public CategoriesController()
        {
            categoryService = new CategoryService();
        }
        // GET: Settings/Categories
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Category> response = categoryService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(long? id)
        {
            CategoryViewModel viewModel = new CategoryViewModel
            {
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetCategoryById(int categoryId)
        {
            var item = categoryService.GetById(categoryId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Category category)
        {
            try
            {
                categoryService.AddOrUpdate(category);
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = category.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult GetByName(string name)
        {
            var user = categoryService.GetByName(name);
            ResponseModel response = new ResponseModel
            {
                StatusCode = (int)(user?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = user?.Id > 0 ? AppResource.EmailAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Delete(int categoryId)
        {
            try
            {
                categoryService.Delete(categoryId);
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