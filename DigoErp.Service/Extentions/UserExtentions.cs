using DigoErp.Repository.Edmx;
using DigoErp.Service.Security;
using System;
using System.Linq;

namespace DigoErp.Service.Extentions
{
    public static class UserExtentions
    {
        private const string DefaultKey = "Independence_is_happines";
        public static Models.User MapFrom(this Tbl_User user)
        {
            return new Models.User
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Photo = user.Photo ?? "/Content/avatar.png",
                FullName = user.FullName,
                BranchId = user.BranchId,
                RoleId = user.RoleId,
                UserRole = user.Tbl_Role?.Name ?? string.Empty,
                BranchName = user.Tbl_Branch?.Name ?? string.Empty,
                Language = user.Language,
                LandingPage = user.LandingPage,
                Password = user.Password = StringCipher.Decrypt(user.Password, DefaultKey),
                Created_At = user.Created_At,
                Updated_At = user.Updated_At,
                DefaultSetting = user.Tbl_Default?.FirstOrDefault()?.MapFrom(),
                IsEnabled = user.IsEnabled
            };
        }

        public static Tbl_User MapFrom(this Models.User user)
        {
            return new Tbl_User
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Photo = user.Photo ?? "/Content/avatar.png",
                FullName = user.FullName,
                BranchId = user.BranchId,
                RoleId = user.RoleId,
                Language = user.Language,
                LandingPage = user.LandingPage,
                Password = StringCipher.Encrypt(user.Password, DefaultKey),
                Created_At = user.Id > 0 ? user.Created_At : DateTime.Now,
                Updated_At = user.Id > 0 ? DateTime.Now : default(DateTime?),
                IsEnabled = user.IsEnabled
            };
        }
    }
}
