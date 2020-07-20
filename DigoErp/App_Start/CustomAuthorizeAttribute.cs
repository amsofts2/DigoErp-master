using DigoErp.Service.Models;
using System.Web;
using System.Web.Mvc;

namespace DigoErp.App_Start
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        private readonly string[] allowedroles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userDetail = (User)HttpContext.Current.Session["UserDetail"];
            userDetail = userDetail ?? new User();
            bool authorize = false;
            foreach (var role in allowedroles)
            {
                switch (role)
                {
                    case "Super Admin":
                        authorize = userDetail.UserRole == "Super Admin";
                        if (authorize)
                            return authorize;
                        break;
                    case "Admin":
                        authorize = userDetail.UserRole == "Admin";
                        if (authorize)
                            return authorize;
                        break;
                }
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}