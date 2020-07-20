using DigoErp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.ViewModels
{
    public class ItemViewModel
    {
        public long Id { get; set; }
       
        public string Name { get; set; }

        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long? TaxId { get; set; }
        public string TaxName { get; set; }
        public decimal? SalePrice { get; set; }
       
        public decimal? PurchasePrice { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public List<Category> catagories { get; set; }
        public List<Tax> taxes { get; set; }
    }
}