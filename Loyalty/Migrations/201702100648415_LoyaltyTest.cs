namespace Loyalty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoyaltyTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.String(),
                        TotalPoints = c.Double(),
                        RedeemedPoints = c.Double(),
                        AvailablePoints = c.Double(),
                        Balance = c.Double(),
                        User_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ProductLines",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Quantity = c.Long(),
                        Date = c.DateTime(),
                        TrackingNumber = c.String(),
                        ProductId = c.Long(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Status = c.Boolean(),
                        Progress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        SellingPrice = c.Double(),
                        Quantity = c.Long(),
                        TotalSold = c.Long(),
                        CostPrice = c.Double(),
                        Status = c.Boolean(),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        MobileNumber = c.String(),
                        Address = c.String(),
                        Role = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ProductLines", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProductLines", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductLines", new[] { "CustomerId" });
            DropIndex("dbo.ProductLines", new[] { "ProductId" });
            DropIndex("dbo.Customers", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.ProductLines");
            DropTable("dbo.Customers");
        }
    }
}
