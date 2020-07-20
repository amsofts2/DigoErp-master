using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;

namespace DigoErp.Service.Extentions
{
    public static class DefaultExtentions
    {
        public static Default MapFrom(this Tbl_Default @default)
        {
            return new Default
            {
                Id = @default.Id,
                AccountId = @default.AccountId,
                CurrencyId = @default.CurrencyId,
                TaxId = @default.TaxId,
                PaymentMethod = @default.PaymentMethod,
                Language = @default.Language,
                RecordsPerPage = @default.RecordsPerPage,
                DefaultCurrency = @default.Tbl_Currency?.MapFrom(),
                CreatedBy = @default.CreatedBy,
                Logo = @default.Logo
            };
        }

        public static Tbl_Default MapFrom(this Default @default)
        {
            return new Tbl_Default
            {
                Id = @default.Id,
                AccountId = @default.AccountId,
                CurrencyId = @default.CurrencyId,
                TaxId = @default.TaxId,
                PaymentMethod = @default.PaymentMethod,
                Language = @default.Language,
                RecordsPerPage = @default.RecordsPerPage,
                CreatedBy = @default.CreatedBy,
                Logo = @default.Logo
            };
        }
    }
}
