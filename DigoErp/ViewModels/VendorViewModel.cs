using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.ViewModels
{
    public class VendorViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? UnPaid { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string TaxNumber { get; set; }
        public long? CurrencyId { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Refrence { get; set; }
        public string CurrencyName { get; set; }

        public List<Currency> Currencies { get; set; }
    }
}