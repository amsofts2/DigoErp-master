using DigoErp.Service.Models;

namespace DigoErp.Areas.Purchases.Models
{
    public class BillDetailViewModel
    {
        public Bill Bill { get; set; }
        public Currency Currency { get; set; }
        public Vendor Vendor { get; set; }
        public Branch Branch { get; set; }
    }
}