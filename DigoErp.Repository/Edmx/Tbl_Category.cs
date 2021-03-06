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
    
    public partial class Tbl_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Category()
        {
            this.Tbl_Item = new HashSet<Tbl_Item>();
            this.Tbl_Transaction = new HashSet<Tbl_Transaction>();
            this.Tbl_Invoice = new HashSet<Tbl_Invoice>();
            this.Tbl_Bill = new HashSet<Tbl_Bill>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Item> Tbl_Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Transaction> Tbl_Transaction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Invoice> Tbl_Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Bill> Tbl_Bill { get; set; }
    }
}
