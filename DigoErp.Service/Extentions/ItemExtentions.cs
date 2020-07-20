using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class ItemExtentions
    {
        public static Item MapFrom(this Tbl_Item item)
        {
            return new Item
            {
                Id = item.Id,
                Name = item.Name,
                CategoryId= item.CategoryId,
                CategoryName = item.Tbl_Category?.Name ?? string.Empty,
                SalePrice = item.SalePrice,
                PurchasePrice = item.PurchasePrice,
                IsEnabled = item.IsEnabled,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At,
                TaxId=item.TaxId,
                TaxName = item.Tbl_Tax?.Name ?? string.Empty,
                Description=item.Description ,
                Picture=item.Picture
            };
        }

        public static Tbl_Item MapFrom(this Item item)
        {
            return new Tbl_Item
            {
                Id = item.Id,
                Name = item.Name,
                CategoryId = item.CategoryId,
                SalePrice = item.SalePrice,
                PurchasePrice = item.PurchasePrice,
                IsEnabled = item.IsEnabled,
                Created_At = item.Id > 0 ? item.Created_At : DateTime.Now,
                Updated_At = item.Id > 0 ? DateTime.Now : default(DateTime?),
                TaxId =item.TaxId,
                Description = item.Description,
                Picture = item.Picture
            };
        }
    }
}
