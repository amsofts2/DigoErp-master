using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new List<InvoiceItem>();
        }
        public long Id { get; set; }
        [Required]
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public long? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        [Required]
        public string InvoiceDate { get; set; }
        [Required]
        public string DueDate { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        public string OrderNumber { get; set; }
        public string Notes { get; set; }
        public string Footer { get; set; }
        [Required]
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Recurring { get; set; }
        public string Attachment { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? GrandTotal { get; set; }
        public string Status { get; set; }
        public decimal? Discount_Percentage { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
        public HttpPostedFileBase file { get; set; }

        #region Currency related properties
        public string SymbolPosition { get; set; }
        public string CurrencySymbol { get; set; }
        #endregion
    }
}
