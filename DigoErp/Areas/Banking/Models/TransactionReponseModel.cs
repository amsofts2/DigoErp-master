using DigoErp.Service.Models;
using System.Collections.Generic;

namespace DigoErp.Areas.Banking.Models
{
    public class TransactionReponseModel
    {
        public long? AccountId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal? ClosingBalance { get; set; }
        public List<ReconciliationTransaction> Transactions { get; set; }
    }
}