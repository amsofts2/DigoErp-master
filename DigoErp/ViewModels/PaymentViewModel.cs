using DigoErp.Service.Enums;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.ViewModels
{
    public class PaymentViewModel
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public long? VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public long? AccountId { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Recurring? Recurring { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string Refrence { get; set; }
        public string Attachment { get; set; }
        public long? BillId { get; set; }
        public string BillNumber { get; set; }

        public List<Vendor> vendors { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Bill> Bills { get; set; }
    }
}