//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigoErp.Repository.Edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_InvoiceItem
    {
        public long Id { get; set; }
        public Nullable<long> InvoiceId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
        public Nullable<long> ItemId { get; set; }
        public Nullable<long> TaxId { get; set; }
    
        public virtual Tbl_Invoice Tbl_Invoice { get; set; }
    }
}