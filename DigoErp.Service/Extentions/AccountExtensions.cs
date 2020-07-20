using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;
using System.Linq;
using DigoErp.Service.Enums;
using System.Threading;
using System.Globalization;

namespace DigoErp.Service.Extentions
{
    public static class AccountExtensions
    {
        static AccountExtensions()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        public static Account MapFrom(this Tbl_Account account)
        {
            var openingBalance = account.OpeningBalance;
            var totalRevenue = account.Tbl_Transaction.Where(x => x.TransactionType == (int)TransactionType.Income).Sum(x => x.Amount);
            var totalExpense = account.Tbl_Transaction.Where(x => x.TransactionType == (int)TransactionType.Expense).Sum(x => x.Amount);
            return new Account
            {
                Id = account.Id,
                AccountName = account.AccountName,
                Number = account.Number,
                IBANNumber = account.IBANNumber,
                CurrencyId = account.CurrencyId,
                CurrencyName = account.Tbl_Currency?.Name ?? string.Empty,
                OpeningBalance = (openingBalance + totalRevenue) - totalExpense,
                BankName = account.BankName,
                BankPhone = account.BankPhone,
                BankAddress = account.BankAddress,
                DefaultAccount = account.DefaultAccount,
                IsEnabled = account.IsEnabled,
                Created_At = account.Created_At,
                Updated_At = account.Updated_At,
                CurrencySymbol = account.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = account.Tbl_Currency?.SymbolPosition ?? string.Empty
            };
        }

        public static Tbl_Account MapFrom(this Account account)
        {
            return new Tbl_Account
            {
                Id = account.Id,
                AccountName = account.AccountName,
                Number = account.Number,
                IBANNumber = account.IBANNumber,
                CurrencyId = account.CurrencyId,
                OpeningBalance = account.OpeningBalance,
                BankName = account.BankName,
                BankPhone = account.BankPhone,
                BankAddress = account.BankAddress,
                DefaultAccount = account.DefaultAccount,
                IsEnabled = account.IsEnabled,
                Created_At = account.Id > 0 ? account.Created_At : DateTime.Now,
                Updated_At = account.Id > 0 ? DateTime.Now : default(DateTime?),
            };
        }
    }
}
