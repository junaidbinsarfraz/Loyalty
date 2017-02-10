namespace Loyalty.LoyaltyTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
