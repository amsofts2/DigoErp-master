﻿using System;

namespace DigoErp.Service.Models
{
    public class InvoiceItem
    {
        public long Id { get; set; }
        public long? InvoiceId { get; set; }
        public long? ItemId { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public long? TaxId { get; set; }
        public decimal? Tax { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
