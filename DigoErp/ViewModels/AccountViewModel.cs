using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.ViewModels
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public long? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public decimal? OpeningBalance { get; set; }
        public string BankName { get; set; }
        public string BankPhone { get; set; }
        public string BankAddress { get; set; }
        public bool? DefaultAccount { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public List<Currency> Currencies { get; set; }
    }
}