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
    
    public partial class Tbl_Tax
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Tax()
        {
            this.Tbl_Item = new HashSet<Tbl_Item>();
            this.Tbl_Defaults = new HashSet<Tbl_Default>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Enabled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Item> Tbl_Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Default> Tbl_Defaults { get; set; }
    }
}
