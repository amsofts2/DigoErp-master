using DigoErp.Service.Models;
using System;
using System.Collections.Generic;

namespace DigoErp.ViewModels
{
    public class TransferViewModel
    {
        public long Id { get; set; }
        public long? FromAccount { get; set; }
        public string FromAccountName { get; set; }
        public long? ToAccount { get; set; }
        public string ToAccountName { get; set; }
        public string Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public int? PaymentMethod { get; set; }
        public string Reference { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        public List<Account> Accounts { get; set; }
    }
}