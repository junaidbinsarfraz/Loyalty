namespace Loyalty.LoyaltyTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Role = "Customer";
            this.Status = true;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
