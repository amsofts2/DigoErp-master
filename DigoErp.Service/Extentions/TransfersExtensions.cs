using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class TransfersExtensions
    {
        public static Transfer MapFrom(this Tbl_Transfer transfer)
        {
            return new Transfer
            {
                Id = transfer.Id,
                FromAccount=transfer.FromAccount,
                ToAccount=transfer.ToAccount,
                FromAccountName = transfer.Tbl_AccountFrom?.AccountName ?? string.Empty,
                ToAccountName = transfer.Tbl_AccountTo?.AccountName ?? string.Empty,
                Amount = transfer.Amount,
                Date = transfer.Date,
                Description = transfer.Description,
                PaymentMethod = transfer.PaymentMethod,
                Reference = transfer.Reference,
                Created_At = transfer.Created_At,
                Updated_At = transfer.Updated_At,
                CurrencySymbol = transfer.Tbl_AccountTo?.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = transfer.Tbl_AccountTo?.Tbl_Currency?.SymbolPosition ?? string.Empty
            };
        }

        public static Tbl_Transfer MapFrom(this Transfer transfer)
        {
            return new Tbl_Transfer
            {
                Id = transfer.Id,
                FromAccount = transfer.FromAccount,
                ToAccount = transfer.ToAccount,
                Amount = transfer.Amount,
                Date = transfer.Date,
                Description = transfer.Description,
                PaymentMethod = transfer.PaymentMethod,
                Reference = transfer.Reference,
                Created_At = transfer.Id > 0 ? transfer.Created_At : DateTime.Now,
                Updated_At = transfer.Id > 0 ? DateTime.Now : default(DateTime?)
            };
        }
    }
}
