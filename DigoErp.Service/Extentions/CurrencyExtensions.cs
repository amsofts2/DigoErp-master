using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class CurrencyExtensions
    {
        public static Tbl_Currency MapFrom(this Currency currency)
        {
            return new Tbl_Currency
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.Code,
                Rate = currency.Rate,
                Precision = currency.Precision,
                Symbol = currency.Symbol,
                SymbolPosition = currency.SymbolPosition,
                DecimalMark = currency.DecimalMark,
                Thousands_Separator = currency.Thousands_Separator,
                Enabled = currency.Enabled,
                Default_Currency = currency.Default_Currency,
                Created_At = currency.Id > 0  ? currency.Created_At : DateTime.Now,
                Updated_At = currency.Id > 0 ? DateTime.Now : default(DateTime?),
            };
        }

        public static Currency MapFrom(this Tbl_Currency currency)
        {
            return new Currency
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.Code,
                Rate = currency.Rate,
                Precision = currency.Precision,
                Symbol = currency.Symbol,
                SymbolPosition = currency.SymbolPosition,
                DecimalMark = currency.DecimalMark,
                Thousands_Separator = currency.Thousands_Separator,
                Enabled = currency.Enabled,
                Default_Currency = currency.Default_Currency,
                Created_At = currency.Created_At,
                Updated_At = currency.Updated_At,
            };
        }
    }
}
