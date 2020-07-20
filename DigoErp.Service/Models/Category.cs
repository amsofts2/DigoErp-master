using System;

namespace DigoErp.Service.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public int? Type { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string TypeName { get; set; }
    }
}
