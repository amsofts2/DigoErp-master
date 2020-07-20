using DigoErp.Repository.Edmx;
using DigoErp.Service.Enums;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class TransactionExtensions
    {
        public static Transaction MapFrom(this Tbl_Transaction transaction)
        {
            return new Transaction
            {
                Id = transaction.Id,
                Date = transaction.Date,
                Amount = transaction.Amount,
                AccountId = transaction.AccountId,
                AccountName = transaction.Tbl_Account?.AccountName ?? string.Empty,
                CustomerId = transaction.CustomerId,
                VendorId = transaction.VendorId,
                CustomerName = transaction.Tbl_Customer?.Name ?? string.Empty,
                VendorName = transaction.Tbl_Vendor?.Name ?? string.Empty,
                BillId = transaction.BillId,
                BillNumber = transaction.Tbl_Bill?.Number ?? string.Empty,
                Description = transaction.Description ?? string.Empty,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Tbl_Category?.Name ?? string.Empty,
                PaymentMethod = transaction.PaymentMethod,
                Recurring = transaction.Recurring,
                Reference = transaction.Reference,
                Attachment = transaction.Attachment,
                Invoice = transaction.Invoice,
                TransactionType = transaction.TransactionType,
                TransactionTypeName = GetEnumValue(transaction),
                Created_At = transaction.Created_At,
                Updated_At = transaction.Updated_At,
                CurrencySymbol = transaction.Tbl_Account?.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = transaction.Tbl_Account?.Tbl_Currency?.SymbolPosition ?? string.Empty
            };
        }

        public static ReconciliationTransaction MapForReconciliation(this Tbl_Transaction transaction)
        {
            var customerName = transaction.Tbl_Customer?.Name ?? string.Empty;
            var vendorName = transaction.Tbl_Vendor?.Name ?? string.Empty;
            return new ReconciliationTransaction
            {
                Id = transaction.Id,
                Date = transaction.Date,
                Deposit = transaction.TransactionType == (int)TransactionType.Income ? transaction.Amount : 0.0M,
                Withdrawal = transaction.TransactionType == (int)TransactionType.Expense ? transaction.Amount : 0.0M,
                Contact = transaction.TransactionType == (int)TransactionType.Income ? customerName : vendorName,
                Description = transaction.Description ?? string.Empty,
                TransactionType = transaction.TransactionType,
                CurrencySymbol = transaction.Tbl_Account?.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = transaction.Tbl_Account?.Tbl_Currency?.SymbolPosition ?? string.Empty
            };
        }

        private static string GetEnumValue(Tbl_Transaction transaction)
        {
            try
            {
                return Enum.Parse(typeof(TransactionType), transaction.TransactionType.ToString()).ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static Tbl_Transaction MapFrom(this Transaction transaction)
        {
            return new Tbl_Transaction
            {
                Id = transaction.Id,
                Date = transaction.Date,
                Amount = transaction.Amount,
                AccountId = transaction.AccountId,
                CustomerId = transaction.CustomerId,
                BillId = transaction.BillId,
                Description = transaction.Description,
                CategoryId = transaction.CategoryId,
                PaymentMethod = transaction.PaymentMethod,
                Recurring = transaction.Recurring,
                Reference = transaction.Reference,
                Attachment = transaction.Attachment,
                Invoice = transaction.Invoice,
                TransactionType = transaction.TransactionType,
                Created_At = transaction.Id > 0 ? transaction.Created_At : DateTime.Now,
                Updated_At = transaction.Id > 0 ? DateTime.Now : default(DateTime?),
            };
        }
    }
}
