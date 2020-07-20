using DigoErp.Service.Enums;
using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace DigoErp.ViewModels
{
    public class RevenueViewModel
    {
        public long Id { get; set; }

        public DateTime? Date { get; set; }

        public string Amount { get; set; }

        public long? AccountId { get; set; }
        public string AccountName { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }

        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Recurring? Recurring { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }
        public string Reference { get; set; }
        public string Attachment { get; set; }
        public long? Invoice { get; set; }
        public string InvoiceName { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public HttpPostedFileBase file { get; set; }

        public List<Account> Accounts { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Category> Categories { get; set; }
    }
}