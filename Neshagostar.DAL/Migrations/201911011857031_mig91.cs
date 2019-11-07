namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig91 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rivals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RivalPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RivalId = c.Guid(nullable: false),
                        TenderId = c.Guid(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .ForeignKey("dbo.Rivals", t => t.RivalId, cascadeDelete: true)
                .Index(t => t.RivalId)
                .Index(t => t.TenderId);
            
            CreateTable(
                "dbo.Tenders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        DateTime = c.String(),
                        GuarantyPrice = c.Double(nullable: false),
                        IsSuccessful = c.Boolean(nullable: false),
                        ReasonForFailure = c.String(),
                        Comments = c.String(),
                        AddedCostAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.TenderItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenderId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        PricePerUnit = c.Double(nullable: false),
                        PricePerKilo = c.Double(nullable: false),
                        CarriageCostPerKilo = c.Double(nullable: false),
                        Comments = c.String(),
                        NominalWeightPerMeter = c.Double(nullable: false),
                        HDPEPrice = c.Double(nullable: false),
                        WasherPrice = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        TotalWeight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RivalPrices", "RivalId", "dbo.Rivals");
            DropForeignKey("dbo.RivalPrices", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.TenderItems", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.TenderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Tenders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TenderItems", new[] { "ProductId" });
            DropIndex("dbo.TenderItems", new[] { "TenderId" });
            DropIndex("dbo.Tenders", new[] { "CustomerId" });
            DropIndex("dbo.RivalPrices", new[] { "TenderId" });
            DropIndex("dbo.RivalPrices", new[] { "RivalId" });
            DropTable("dbo.TenderItems");
            DropTable("dbo.Tenders");
            DropTable("dbo.RivalPrices");
            DropTable("dbo.Rivals");
        }
    }
}
