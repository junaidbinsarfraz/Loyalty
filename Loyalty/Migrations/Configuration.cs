namespace Loyalty.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Loyalty.LoyaltyTest;

    internal sealed class Configuration : DbMigrationsConfiguration<Loyalty.LoyaltyTest.LoyaltyTest>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Loyalty.LoyaltyTest.LoyaltyTest context)
        {

            User user = new User
            {
                Name = "Admin",
                Status = true,
                Username = "admin",
                Password = "admin",
                Address = "Address",
                Role = "Admin",
                MobileNumber = "02838265321"
            };

            //context.Users.Add(user);

            context.Users.AddOrUpdate(u => u.Username, user);

            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
