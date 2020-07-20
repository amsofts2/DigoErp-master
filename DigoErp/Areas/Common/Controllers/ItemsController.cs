using System.IO;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using System.Net;
using System.Web.Mvc;
using DigoErp.Controllers;
using DigoErp.App_Start;

namespace DigoErp.Areas.Common.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class ItemsController : BaseController
    {
        private readonly ItemService itemService;
        private readonly CategoryService categoryService;
        private readonly TaxService taxService;
        public ItemsController()
        {
            itemService = new ItemService();
            categoryService = new CategoryService();
            taxService = new TaxService();
        }
        // GET: Common/Items
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Item> response = itemService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(long? id)
        {
            ItemViewModel viewModel = new ItemViewModel
            {
                catagories = categoryService.GetAll(),
                taxes = taxService.GetAll(),
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetItemById(int itemId)
        {
            var item = itemService.GetById(itemId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Item item)
        {
            try
            {
                var logoName = Path.GetFileName(item.file.FileName);
                var folderPath = Server.MapPath("~/images/items");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var path = Path.Combine(folderPath, logoName);
                item.file.SaveAs(path);
                item.Picture = "images/items/" + logoName;
            }
            catch (System.Exception)
            {
            }

            try
            {
                itemService.AddOrUpdate(item);
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = item.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
            var item = itemService.GetByName(name);
            ResponseModel response = new ResponseModel
            {
                StatusCode = (int)(item?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = item?.Id > 0 ? AppResource.ItemAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int itemId)
        {
            try
            {
                itemService.Delete(itemId);
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