using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;

namespace DigoErp.Service.Extentions
{
    public static class RoleExtension
    {
        public static Role MapFrom(this Tbl_Role role)
        {
            return new Role
            {
                Id=role.Id,
                Name=role.Name,
                Code=role.Code,
                Description=role.Description
            };
        }

        public static Tbl_Role MapFrom(this Role role)
        {
            return new Tbl_Role
            {
              Id=role.Id,
              Name=role.Name,
              Code=role.Code,
              Description=role.Description
            };
        }
    }
}
