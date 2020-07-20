using DigoErp.Service.Models;
using System.Collections.Generic;

namespace DigoErp.ViewModels
{
    public class BillViewModel
    {
        public BillViewModel()
        {
            Bill = new Bill();
            DefaultSetting = new Default();
        }
        public Bill Bill { get; set; }
        public Default DefaultSetting { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Category> Categories { get; set; }
        public List<Item> Items { get; set; }
        public List<Tax> Taxes { get; set; }
    }
}