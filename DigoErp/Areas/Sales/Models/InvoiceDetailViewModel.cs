using DigoErp.Service.Models;

namespace DigoErp.Areas.Sales.Models
{
    public class InvoiceDetailViewModel
    {
        public Invoice Invoice { get; set; }
        public Currency Currency { get; set; }
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
    }
}