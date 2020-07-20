using System.IO;
using System.Net;
using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;

namespace DigoErp.Areas.Common.Controllers
{
    [CustomAuthorize("Super Admin")]
    public class BranchesController : BaseController
    {
        private readonly BranchService _branchService;
        public BranchesController()
        {
            _branchService = new BranchService();
        }

        // GET: Branch
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetResponse(DataTableSearchModel searchModel)
        {
            var respsonse = _branchService.GetResponse(searchModel);
            return Json(respsonse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Branch branch)
        {
            try
            {
                try
                {
                    string logoName = Path.GetFileName(branch.file.FileName);
                    var folderPath = Server.MapPath("~/images/branches");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string path = Path.Combine(folderPath, logoName);
                    branch.file.SaveAs(path);
                    branch.Logo = "images/branches/" + logoName;
                }
                catch (System.Exception)
                {
                }

                _branchService.AddOrUpdate(branch);
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = branch.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult GetById(int branchId)
        {
            var batch = _branchService.GetById(branchId);
            return Json(batch, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int branchId)
        {
            try
            {
                _branchService.Delete(branchId);
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