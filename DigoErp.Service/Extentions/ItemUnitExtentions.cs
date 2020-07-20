using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class ItemUnitExtentions
    {
        public static Tbl_ItemUnit MapFrom(this ItemUnit item)
        {
            return new Tbl_ItemUnit
            {
                Id = item.Id,
                NameAr = item.NameAr,
                NameEn = item.NameEn,
                Created_At = item.Id <= 0 ? DateTime.Now : default(DateTime?),
                Updated_At = item.Id > 0 ? DateTime.Now : default(DateTime?),
            };
        }
        public static ItemUnit MapFrom(this Tbl_ItemUnit item)
        {
            return new ItemUnit
            {
                Id = item.Id,
                NameAr = item.NameAr,
                NameEn = item.NameEn,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }
    }
}
