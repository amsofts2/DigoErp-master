using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int? BranchId { get; set; }
        [Required]
        public int? RoleId { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Password { get; set; }
        public string BranchName { get; set; }
        public string UserRole { get; set; }
        public string ResetPasswordCode { get; set; }
        public string Language { get; set; }
        public string LandingPage { get; set; }
        public bool? IsEnabled { get; set; }
        public HttpPostedFileBase file { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public Default DefaultSetting { get; set; }
    }
}
