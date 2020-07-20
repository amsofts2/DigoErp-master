using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class Bill
    {
        public Bill()
        {
            Bill_Items = new List<Bill_Item>();
        }
        public long Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Number { get; set; }
        [Required]
        public long? VendorId { get; set; }
        
        public string VendorName { get; set; }
        public decimal? Amount { get; set; }
        [Required]
        public string BillDate { get; set; }
        [Required]
        public string DueDate { get; set; }
        public string Status { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        [Required]
        public long? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string Notes { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string OrderNumber { get; set; }
        [Required]
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Recurring { get; set; }
        public string Attachment { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? Discount_Percentage { get; set; }
        public List<Bill_Item> Bill_Items { get; set; } 
        public HttpPostedFileBase file { get; set; }

        #region Currency related properties
        public string SymbolPosition { get; set; }
        public string CurrencySymbol { get; set; }
        #endregion
    }
}
