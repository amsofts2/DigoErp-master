using DigoErp.Repository.Edmx;
using DigoErp.Service.Enums;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class PaymentExtension
    {
        public static Payment MapForPayment(this Tbl_Transaction payment)
        {
            return new Payment
            {
                Id = payment.Id,
                Date = payment.Date,
                VendorId = payment.VendorId,
                VendorName = payment.Tbl_Vendor?.Name ?? string.Empty,
                BillId = payment.BillId,
                BillNumber = payment.Tbl_Bill?.Number ?? string.Empty,
                Amount = payment.Amount,
                Description = payment.Description,
                Recurring = payment.Recurring,
                Refrence = payment.Reference,
                PaymentMethod = payment.PaymentMethod,
                Created_At = payment.Created_At,
                Updated_At = payment.Updated_At,
                AccountId = payment.AccountId,
                AccountName = payment.Tbl_Account?.AccountName ?? string.Empty,
                CategoryId = payment.CategoryId,
                TransactionType = (int)TransactionType.Expense,
                CategoryName = payment.Tbl_Category?.Name ?? string.Empty,
                Attachment = payment.Attachment ?? "/Content/avatar.png",
                CurrencySymbol = payment.Tbl_Account?.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = payment.Tbl_Account?.Tbl_Currency?.SymbolPosition ?? string.Empty
            };
        }

        public static Tbl_Transaction MapFrom(this Payment payment)
        {
            return new Tbl_Transaction
            {
                Id = payment.Id,
                Date = payment.Date,
                VendorId = payment.VendorId,
                Amount = payment.Amount,
                Description = payment.Description,
                Recurring = payment.Recurring,
                PaymentMethod = payment.PaymentMethod,
                AccountId = payment.AccountId,
                Created_At = payment.Id > 0 ? payment.Created_At : DateTime.Now,
                Updated_At = payment.Id > 0 ? DateTime.Now : default(DateTime?),
                CategoryId = payment.CategoryId,
                Attachment = payment.Attachment,
                Reference = payment.Refrence,
                TransactionType = (int)TransactionType.Expense,
                BillId = payment.BillId
            };
        }
    }
}
