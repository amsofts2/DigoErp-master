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
    
    public partial class Tbl_Default
    {
        public long Id { get; set; }
        public Nullable<long> AccountId { get; set; }
        public Nullable<long> CurrencyId { get; set; }
        public Nullable<long> TaxId { get; set; }
        public Nullable<int> PaymentMethod { get; set; }
        public string Language { get; set; }
        public Nullable<int> RecordsPerPage { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public string Logo { get; set; }
    
        public virtual Tbl_Account Tbl_Account { get; set; }
        public virtual Tbl_Currency Tbl_Currency { get; set; }
        public virtual Tbl_Tax Tbl_Tax { get; set; }
        public virtual Tbl_User Tbl_User { get; set; }
    }
}