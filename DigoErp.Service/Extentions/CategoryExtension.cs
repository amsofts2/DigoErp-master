using DigoErp.Repository.Edmx;
using DigoErp.Service.Enums;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class CategoryExtension
    {
        public static Category MapFrom(this Tbl_Category category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                Type = category.Type,
                TypeName = GetEnumValue(category),
                Enabled = category.Enabled,
                Created_At = category.Created_At,
                Updated_At = category.Updated_At,
            };
        }

        private static string GetEnumValue(Tbl_Category category)
        {
            try
            {
                return Enum.Parse(typeof(Types), category.Type.ToString()).ToString();
            }
            catch (Exception)
            {
               return string.Empty;
            }
        }

        public static Tbl_Category MapFrom(this Category category)
        {
            return new Tbl_Category
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                Type = category.Type,
                Enabled = category.Enabled,
                Created_At = category.Id > 0 ? category.Created_At :  DateTime.Now,
                Updated_At = category.Id > 0 ? DateTime.Now : default(DateTime?),
            };
        }
    }
}
