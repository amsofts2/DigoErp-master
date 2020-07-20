using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace DigoErp.Areas.Auth.Controllers
{
    [CustomAuthorize("Super Admin")]
    public class RolesController : BaseController
    {
        private readonly RoleService roleService;

        public RolesController()
        {
            roleService = new RoleService();
        }
        // GET: Auth/Roles
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            DataTableResponse<Role> response = roleService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(long? id)
        {
            RoleViewModel viewModel = new RoleViewModel
            {
                Id = id ?? 0
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetRoleById(int roleId)
        {
            var role = roleService.GetById(roleId);
            return Json(role, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Role role)
        {
            try
            {
                roleService.AddOrUpdate(role);

                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = role.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult GetByRoleName(string rolename)
        {
            var role = roleService.GetByRoleName(rolename);
            ResponseModel response = new ResponseModel
            {
                StatusCode = (int)(role?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = role?.Id > 0 ? AppResource.RoleAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int roleId)
        {
            try
            {
                roleService.Delete(roleId);
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