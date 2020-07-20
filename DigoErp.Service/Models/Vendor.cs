using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class Vendor
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public decimal? UnPaid { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string TaxNumber { get; set; }
        [Required]
        public long? CurrencyId { get; set; }
        public string Website { get; set; }
        [Required]
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Refrence { get; set; }
        public string AccountNumber { get; set; }
        public string SadadNumber { get; set; }
        public string CurrencyName { get; set; }
        public HttpPostedFileBase file { get; set; }

    }
}
