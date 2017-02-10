namespace Loyalty.LoyaltyTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoyaltyTest : DbContext
    {
        public LoyaltyTest()
            : base("name=LoyaltyTest")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProductLine> ProductLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ProductLines)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductLines)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Customer)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
