using DigoErp.Service.Models;
using System;
using System.Collections.Generic;

namespace DigoErp.ViewModels
{
    public class CustomerViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public bool? IsEnabled { get; set; }
        public string Refrence { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? BranchId { get; set; }
        public long? CurrencyId { get; set; }
        public string website { get; set; }
        public bool? CanLogin { get; set; }

        public string BranchName { get; set; }
        public string CurrencyName { get; set; }

        public List<Currency> Currencies { get; set; }
    }
}