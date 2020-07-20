using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class Revenue
    {
        public long Id { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public long? AccountId { get; set; }
        public string AccountName { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        [Required]
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Recurring { get; set; }
        [Required]
        public int? PaymentMethod { get; set; }
        public string Reference { get; set; }
        public string Attachment { get; set; }
        public long? Invoice { get; set; }
        public string InvoiceName { get; set; }
        public int TransactionType { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public HttpPostedFileBase file { get; set; }


        #region Currency related properties
        public string SymbolPosition { get; set; }
        public string CurrencySymbol { get; set; }
        #endregion
    }
}
