using System.Web;

namespace DigoErp.Service.Models
{
    public class Default
    {
        public Default()
        {
            DefaultCurrency = new Currency();
        }
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public long? CurrencyId { get; set; }
        public long? TaxId { get; set; }
        public int? PaymentMethod { get; set; }
        public string Language { get; set; }
        public int? RecordsPerPage { get; set; }
        public long? CreatedBy { get; set; }
        public Currency DefaultCurrency { get; set; }
        public string Logo { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}
