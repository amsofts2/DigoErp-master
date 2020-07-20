using System;
using System.ComponentModel.DataAnnotations;

namespace DigoErp.Service.Models
{
    public class Customer
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public bool? IsEnabled { get; set; }
        public string Refrence { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        [Required]
        public int? BranchId { get; set; }
        public long? CurrencyId { get; set; }
        public string Website { get; set; }
        public bool? CanLogin { get; set; }

        public string BranchName { get; set; }
        public string CurrencyName { get; set; }
        public string AccountNumber { get; set; }
        public string SadadNumber { get; set; }
    }
}
