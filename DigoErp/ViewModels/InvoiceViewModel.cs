using DigoErp.Service.Models;
using System.Collections.Generic;

namespace DigoErp.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            Invoice  = new Invoice();
            DefaultSetting = new Default();
        }
        public Invoice Invoice { get; set; }
        public Default DefaultSetting { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Category> Categories { get; set; }
        public List<Item> Items { get; set; }
        public List<Tax> Taxes { get; set; }
        public string Invoice_Number { get; set; }
    }
}