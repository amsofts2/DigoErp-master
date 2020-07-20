using System;
using System.ComponentModel.DataAnnotations;

namespace DigoErp.Service.Models
{
    public class Transfer
    {
        public long Id { get; set; }
        [Required]
        public long? FromAccount { get; set; }
        public string FromAccountName { get; set; }
        [Required]
        public long? ToAccount { get; set; }
        public string ToAccountName { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string Date { get; set; }
        public string Description { get; set; }
        [Required]
        public int? PaymentMethod { get; set; }
        public string Reference { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        #region Currency related properties
        public string SymbolPosition { get; set; }
        public string CurrencySymbol { get; set; }
        #endregion
    }
}
