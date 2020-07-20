using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class Payment
    {
        public long Id { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public long? VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        [Required]
        public long? AccountId { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        [Required]
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Recurring { get; set; }
        public int? PaymentMethod { get; set; }
        public string Refrence { get; set; }
        public string Attachment { get; set; }
        public int TransactionType { get; set; }
        [Required]
        public long? BillId { get; set; }
        public string BillNumber { get; set; }
        #region Currency related properties
        public string SymbolPosition { get; set; }
        public string CurrencySymbol { get; set; }
        #endregion
        public HttpPostedFileBase file { get; set; }
    }
}
