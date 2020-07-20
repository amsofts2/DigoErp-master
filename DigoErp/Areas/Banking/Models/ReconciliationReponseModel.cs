using DigoErp.Service.Models;
using System.Collections.Generic;

namespace DigoErp.Areas.Banking.Models
{
    public class ReconciliationReponseModel
    {
        public List<Account> Accounts { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}