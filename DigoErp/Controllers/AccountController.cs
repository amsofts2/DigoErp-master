using System;
using System.Web.Mvc;
using System.Web.Security;
using DigoErp.App_Start;
using DigoErp.Models;
using DigoErp.Resources.App_Resources;
using DigoErp.Service.Security;
using DigoErp.Service.Services;

namespace DigoErp.Controllers
{
    [CustomAuthorize("Super Admin", "Admin")]
    public class AccountController : BaseController
    {
        private readonly UserService userService;
        private const string DefaultKey = "Independence_is_happines";
        public AccountController()
        {
            userService = new UserService();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = userService.Login(model.Email, model.Password);

                if (!string.IsNullOrEmpty(result.Email))
                {
                    Session["UserDetail"] = result;
                    Session["UserName"] = result.FullName.ToUpperInvariant();
                    Session["UserRole"] = result.UserRole;
                    Session["Default_Settings"] = result.DefaultSetting;
                    Session["_culture"] = result.Language;

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("~/");
                }
                ModelState.AddModelError("", UserRes.Invalidloginattempt);
                return View(model);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            EmailService emailService = new EmailService();
            string resetCode = StringCipher.Encrypt(Guid.NewGuid().ToString(), DefaultKey);
            resetCode = resetCode.Replace("+", "").Trim();
            var verifyUrl = "/account/resetPassword?resetCode=" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var user = userService.GetByEmail(email);
            if (user != null)
            {
                user.ResetPasswordCode = resetCode;

                var subject = "Password Reset Request";
                var body = "Hi " + user.FullName + ", <br/>We have received your request to reset your password. Please click the link below to complete the reset: " +
                     " <br/><br/><a href='" + link + "'>Reset My Password</a> <br/><br/>" +
                     "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";

                emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                {
                    Subject = subject,
                    Body = body,
                    Destination = user.Email
                });
                userService.ResetPasswordCode(user);
                ViewBag.Message = "Reset password link has been sent to your email.";
            }
            else
            {
                ViewBag.Message = "User doesn't exists.";
                return View();
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string resetCode)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            if (string.IsNullOrWhiteSpace(resetCode))
            {
                ViewBag.Message = "";
                return View();
            }
            var user = userService.GetByResetPasswordCode(resetCode);
            if (user != null)
            {
                
                model.ResetCode = resetCode;
                return View(model);
            }
            else
            {
                ViewBag.Message = "";
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                if(model.Password != model.ConfirmPassword)
                {
                    ViewBag.Message = "Password and confirmed password must be equals.";
                    return View(model);
                }
                var user = userService.GetByResetPasswordCode(model.ResetCode);
                if (user != null)
                {
                    user.Password = StringCipher.Encrypt(model.Password, DefaultKey);
                    user.ResetPasswordCode = "";
                    userService.UpdateBySqlQuery("UPDATE Tbl_User SET ResetPasswordCode = '" + user.ResetPasswordCode + "' , Password ='"+  user.Password +"' WHERE Id=" + user.Id);
                    return Redirect("~/account/login");
                }
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage;
                    }
                }
            }
            ViewBag.Message = message;
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect("~/account/login");
        }
    }
}