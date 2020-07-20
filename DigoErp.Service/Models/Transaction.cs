using System;

namespace DigoErp.Service.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public decimal? Amount { get; set; }
        public long? AccountId { get; set; }
        public long? CustomerId { get; set; }
        public long? VendorId { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        public string BillNumber { get; set; }
        public long? CategoryId { get; set; }
        public int? Recurring { get; set; }
        public int? PaymentMethod { get; set; }
        public string Reference { get; set; }
        public string Attachment { get; set; }
        public long? Invoice { get; set; }
        public long? BillId { get; set; }
        public int? TransactionType { get; set; }
        public string TransactionTypeName { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        #region Currency related properties
        public string SymbolPosition { get; set; }
        public string CurrencySymbol { get; set; }
        public string VendorName { get; set; }

        #endregion
    }
}
