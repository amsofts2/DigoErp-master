using System;
using System.ComponentModel.DataAnnotations;

namespace DigoErp.Service.Models
{
    public class Account
    {
        public long Id { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string Number { get; set; }
        public string IBANNumber { get; set; }
        [Required]
        public long? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        [Required]
        public decimal? OpeningBalance { get; set; }
        public string BankName { get; set; }
        public string BankPhone { get; set; }
        public string BankAddress { get; set; }
        public bool? DefaultAccount { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        #region Currency related properties
        public string SymbolPosition { get; set; }
        public string CurrencySymbol { get; set; }
        #endregion
    }
}
