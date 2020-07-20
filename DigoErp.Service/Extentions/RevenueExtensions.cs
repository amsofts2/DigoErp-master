using DigoErp.Repository.Edmx;
using DigoErp.Service.Enums;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class RevenueExtensions
    {
        public static Revenue MapForRevenue(this Tbl_Transaction revenue)
        {
            return new Revenue
            {
                Id = revenue.Id,
                Date = revenue.Date,
                Amount = revenue.Amount,
                AccountId = revenue.AccountId,
                AccountName = revenue.Tbl_Account?.AccountName ?? string.Empty,
                CustomerId = revenue.CustomerId,
                CustomerName = revenue.Tbl_Customer?.Name ?? string.Empty,
                Description = revenue.Description,
                CategoryId = revenue.CategoryId,
                CategoryName = revenue.Tbl_Category?.Name ?? string.Empty,
                Recurring = revenue.Recurring,
                PaymentMethod = revenue.PaymentMethod,
                Reference = revenue.Reference,
                Attachment = revenue.Attachment,
                Invoice = revenue.Invoice,
                TransactionType = (int)TransactionType.Income,
                Created_At = revenue.Created_At,
                Updated_At = revenue.Updated_At,
                CurrencySymbol = revenue.Tbl_Account?.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = revenue.Tbl_Account?.Tbl_Currency?.SymbolPosition ?? string.Empty
            };
        }

        public static Tbl_Transaction MapFrom(this Revenue revenue)
        {
            return new Tbl_Transaction
            {
                Id = revenue.Id,
                Date = revenue.Date,
                Amount = revenue.Amount,
                AccountId = revenue.AccountId,
                CustomerId = revenue.CustomerId,
                Description = revenue.Description,
                CategoryId = revenue.CategoryId,
                PaymentMethod = revenue.PaymentMethod,
                Recurring = revenue.Recurring,
                Reference = revenue.Reference,
                Attachment = revenue.Attachment,
                Invoice = revenue.Invoice,
                TransactionType = revenue.TransactionType,
                Created_At = revenue.Id > 0 ? revenue.Created_At : DateTime.Now,
                Updated_At = revenue.Id > 0 ? DateTime.Now : default(DateTime?)
            };
        }
    }
}
