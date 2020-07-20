using System.IO;
using System.Net;
using System.Web.Mvc;
using DigoErp.App_Start;
using DigoErp.Controllers;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Models;
using DigoErp.Service.Services;
using DigoErp.ViewModels;

namespace DigoErp.Areas.Auth.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class UsersController : BaseController
    {
        private readonly UserService userService;
        private readonly BranchService branchService;
        private readonly RoleService roleService;
        public UsersController()
        {
            userService = new UserService();
            branchService = new BranchService();
            roleService = new RoleService();
        }

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(DataTableSearchModel searchModel)
        {
            var response = userService.GetResponse(searchModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(int? id)
        {
            var viewModel = new UserViewModel
            {
                Branches = branchService.GetAll(),
                Roles = userService.GetRoles(),
                Id = id ?? 0
            };
            return View(viewModel);
        }

        public new ActionResult Profile()
        {
            var user = Session["UserDetail"] as User ?? new User();
            var viewModel = new UserViewModel
            {
                Branches = branchService.GetAll(),
                Roles = userService.GetRoles(),
                Id = user.Id
            };
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult AddOrUpdate(User user)
        {
            try
            {
                try
                {
                    var logoName = Path.GetFileName(user.file.FileName);
                    var folderPath = Server.MapPath("~/images/users");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    var path = Path.Combine(folderPath, logoName);
                    user.file.SaveAs(path);
                    user.Photo = "images/users/" + logoName;
                }
                catch (System.Exception)
                {
                }

                userService.AddOrUpdate(user);
                if (user.Id > 0)
                {
                    if (LogedInUser.Id == user.Id)
                    {
                        var userDetails = userService.GetById(user.Id);
                        if (string.IsNullOrEmpty(userDetails.UserRole) && userDetails.RoleId != null && userDetails.RoleId > 0)
                        {
                            userDetails.UserRole = roleService.GetById((int)userDetails.RoleId)?.Name ?? string.Empty;
                        }
                        Session["_culture"] = user.Language;
                        Session.Remove("UserDetail");
                        Session["UserDetail"] = userDetails;
                    }
                }
                var responseModel = new ResponseModel
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageAr = user.Id > 0 ? AppResource.UpdatedSuccessfully : AppResource.SavedSuccessfully
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
        public ActionResult GetUserById(int userId)
        {
            var user = userService.GetById(userId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetByEmail(string email)
        {
            var user = userService.GetByEmail(email);
            var response = new ResponseModel
            {
                StatusCode = (int)(user?.Id > 0 ? HttpStatusCode.Found : HttpStatusCode.NotFound),
                MessageAr = user?.Id > 0 ? AppResource.EmailAlreadyTaken : string.Empty
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int userId)
        {
            try
            {
                userService.Delete(userId);
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