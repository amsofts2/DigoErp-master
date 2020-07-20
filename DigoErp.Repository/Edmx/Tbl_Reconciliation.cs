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
    
    public partial class Tbl_Reconciliation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Reconciliation()
        {
            this.Tbl_Reconciliation_Transactions = new HashSet<Tbl_Reconciliation_Transaction>();
        }
    
        public long Id { get; set; }
        public Nullable<long> AccountId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Nullable<decimal> ClosingBalance { get; set; }
        public string Status { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
    
        public virtual Tbl_Account Tbl_Account { get; set; }
        public virtual Tbl_User Tbl_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Reconciliation_Transaction> Tbl_Reconciliation_Transactions { get; set; }
    }
}
