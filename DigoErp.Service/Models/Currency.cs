using System;

namespace DigoErp.Service.Models
{
    public class Currency
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
        public int? Precision { get; set; }
        public string Symbol { get; set; }
        public string SymbolPosition { get; set; }
        public string DecimalMark { get; set; }
        public string Thousands_Separator { get; set; }
        public bool? Enabled { get; set; }
        public bool? Default_Currency { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
