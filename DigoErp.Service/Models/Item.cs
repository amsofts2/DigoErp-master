using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class Item
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public decimal? SalePrice { get; set; }
        [Required]
        public long? TaxId { get; set; }
        public string TaxName { get; set; }
        [Required]
        public decimal? PurchasePrice { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public HttpPostedFileBase file { get; set; }
    }
}
