using DigoErp.Service.Models;
using System.Collections.Generic;
using System.Web;

namespace DigoErp.ViewModels
{
    public class UserViewModel
    {
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RoleId { get; set; }
        public int? IsEnabled { get; set; }
        public HttpPostedFileBase file { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Role> Roles { get; set; }
        public User User { get; set; }
    }
}