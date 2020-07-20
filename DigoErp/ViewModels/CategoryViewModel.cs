using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigoErp.ViewModels
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public int? Type { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}