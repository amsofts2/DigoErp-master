using System;
using System.Collections.Generic;

namespace DigoErp.Service.Models
{
    public class Reconciliation
    {
        public Reconciliation()
        {
            Transactions = new List<ReconciliationTransaction>();
        }
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal? ClosingBalance { get; set; }
        public string Status { get; set; }
        public long? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string AccountName { get; set; }
        public List<ReconciliationTransaction> Transactions { get; set; }
    }
}
