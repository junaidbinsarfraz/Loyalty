//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Loyalty
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.TotalPoints = 0D;
            this.RedeemedPoints = 0D;
            this.AvailablePoints = 0D;
            this.Balance = 0D;
            this.ProductLines = new HashSet<ProductLine>();
        }
    
        public int Id { get; set; }
        public string MemberId { get; set; }
        public Nullable<double> TotalPoints { get; set; }
        public Nullable<double> RedeemedPoints { get; set; }
        public Nullable<double> AvailablePoints { get; set; }
        public Nullable<double> Balance { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductLine> ProductLines { get; set; }
    }
}
