using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class CustomerExtentions
    {
        public static Tbl_Customer MapFrom(this Customer customer)
        {
            return new Tbl_Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                CurrencyId = customer.CurrencyId,
                PhoneNumber = customer.PhoneNumber,
                TaxNumber = customer.TaxNumber,
                Address = customer.Address,
                IsEnabled = customer.IsEnabled,
                Refrence = customer.Refrence,
                Created_At = customer.Id > 0 ? customer.Created_At : DateTime.Now,
                Updated_At = customer.Id > 0 ? DateTime.Now: default(DateTime?),
                BranchId = customer.BranchId,
                Website = customer.Website,
                CanLogin = customer.CanLogin,
                AccountNumber = customer.AccountNumber,
                SadadNumber = customer.SadadNumber
            };
        }

        public static Customer MapFrom(this Tbl_Customer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                CurrencyId = customer.CurrencyId,
                TaxNumber = customer.TaxNumber,
                Address = customer.Address,
                IsEnabled = customer.IsEnabled,
                Refrence = customer.Refrence,
                Created_At = customer.Created_At,
                Updated_At = customer.Updated_At,
                BranchId = customer.BranchId,
                Website = customer.Website,
                CanLogin = customer.CanLogin,
                AccountNumber = customer.AccountNumber,
                SadadNumber = customer.SadadNumber
            };
        }
    }
}
