namespace Loyalty.LoyaltyTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.SellingPrice = 0D;
            this.Quantity = 0;
            this.TotalSold = 0;
            this.CostPrice = 0D;
            this.Status = true;
            this.ProductLines = new HashSet<ProductLine>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<double> SellingPrice { get; set; }
        public Nullable<long> Quantity { get; set; }
        public Nullable<long> TotalSold { get; set; }
        public Nullable<double> CostPrice { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Size { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductLine> ProductLines { get; set; }
    }
}