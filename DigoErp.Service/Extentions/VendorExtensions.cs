using DigoErp.Repository.Edmx;
using System;

namespace DigoErp.Service.Extentions
{
    public static class VendorExtensions
    {
        public static Models.Vendor MapFrom(this Tbl_Vendor vendor)
        {
            return new Models.Vendor
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Email = vendor.Email,
                PhoneNumber = vendor.PhoneNumber,
                UnPaid = vendor.UnPaid,
                IsEnabled = vendor.IsEnabled ,
                Photo = vendor.Photo ?? "/Content/avatar.png" ,
                Created_At = vendor.Created_At,
                Updated_At = vendor.Updated_At,
                TaxNumber=vendor.TaxNumber,
                CurrencyId=vendor.CurrencyId,
                CurrencyName = vendor.Tbl_Currency?.Name ?? string.Empty,
                Website = vendor.Website,
                Address = vendor.Address,
                Refrence = vendor.Refrence,
                AccountNumber = vendor.AccountNumber,
                SadadNumber = vendor.SadadNumber,
            };
        }

        public static Tbl_Vendor MapFrom(this Models.Vendor vendor)
        {
            return new Tbl_Vendor
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Email = vendor.Email,
                PhoneNumber = vendor.PhoneNumber,
                UnPaid = vendor.UnPaid,
                Photo = vendor.Photo ?? "/Content/avatar.png",
                IsEnabled = vendor.IsEnabled,
                Created_At = vendor.Id > 0 ? vendor.Created_At :  DateTime.Now,
                Updated_At = vendor.Id > 0 ? DateTime.Now : default(DateTime?),
                TaxNumber = vendor.TaxNumber,
                CurrencyId = vendor.CurrencyId,
                Website = vendor.Website,
                Address = vendor.Address,
                Refrence = vendor.Refrence,
                AccountNumber = vendor.AccountNumber,
                SadadNumber = vendor.SadadNumber
            };
        }
    }
}
