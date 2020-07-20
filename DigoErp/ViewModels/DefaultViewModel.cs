using System.Collections.Generic;
using DigoErp.Service.Models;

namespace DigoErp.ViewModels
{
    public class DefaultViewModel
    {
        public List<Account> Accounts { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Tax> Taxs { get; set; }
        public Default @Default { get; set; }
    }
}