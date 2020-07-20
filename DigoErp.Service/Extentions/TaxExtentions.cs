using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;

namespace DigoErp.Service.Extentions
{
    public static class TaxExtentions
    {
        public static Tax MapFrom(this Tbl_Tax tax)
        {
            return new Tax
            {
                Id = tax.Id,
                Name = tax.Name,
                Rate = tax.Rate,
                Type = tax.Type,
                Enabled = tax.Enabled
            };
        }

        public static Tbl_Tax MapFrom(this Tax tax)
        {
            return new Tbl_Tax
            {
                Id = tax.Id,
                Name = tax.Name,
                Rate = tax.Rate,
                Type = tax.Type,
                Enabled = tax.Enabled
            };
        }
    }
}
