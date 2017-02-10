namespace Loyalty.LoyaltyTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductLine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductLine()
        {
            this.Quantity = 0;
            this.ProductId = 1;
            this.CustomerId = 1;
            this.Status = true;
            this.Progress = "Processing";
        }

        public long Id { get; set; }
        public Nullable<long> Quantity { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TrackingNumber { get; set; }
        public long ProductId { get; set; }
        public int CustomerId { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Progress { get; set; }

        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
