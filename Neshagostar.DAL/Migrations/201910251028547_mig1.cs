namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Callers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Tel = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecievedCalls",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CallerId = c.Guid(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        Date = c.String(),
                        Time = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Callers", t => t.CallerId, cascadeDelete: true)
                .Index(t => t.CallerId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        DepartmentId = c.Guid(),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Personnel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Personnel_Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex")
                .Index(t => t.Personnel_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        MobileNumber = c.String(),
                        TelephoneNumber = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        Date = c.String(),
                        Comments = c.String(),
                        InquiryAddress = c.String(),
                        InquiryTel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Inquiries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        DateTime = c.String(),
                        IsSuccessful = c.Boolean(nullable: false),
                        ReasonForFailure = c.String(),
                        Comments = c.String(),
                        HasAddedCost = c.Boolean(nullable: false),
                        AddedCostAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.InquiryItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InquiryId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        PricePerUnit = c.Double(nullable: false),
                        PricePerKilo = c.Double(nullable: false),
                        IsSuccessful = c.Boolean(nullable: false),
                        ReasonForFailure = c.String(),
                        Comments = c.String(),
                        NominalWeightPerMeter = c.Double(nullable: false),
                        HDPEPrice = c.Double(nullable: false),
                        WasherPrice = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        TotalWeight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquiries", t => t.InquiryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.InquiryId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductCategoryId = c.Guid(nullable: false),
                        PipeProfileId = c.Guid(nullable: false),
                        PipeDiameterId = c.Guid(nullable: false),
                        RingStiffnessId = c.Guid(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PipeDiameters", t => t.PipeDiameterId, cascadeDelete: true)
                .ForeignKey("dbo.PipeProfiles", t => t.PipeProfileId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.RingStiffnesses", t => t.RingStiffnessId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.PipeProfileId)
                .Index(t => t.PipeDiameterId)
                .Index(t => t.RingStiffnessId);
            
            CreateTable(
                "dbo.PipeDiameters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PipeProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        SendingUnit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RingStiffnesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.InquiryItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "RingStiffnessId", "dbo.RingStiffnesses");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "PipeProfileId", "dbo.PipeProfiles");
            DropForeignKey("dbo.Products", "PipeDiameterId", "dbo.PipeDiameters");
            DropForeignKey("dbo.InquiryItems", "InquiryId", "dbo.Inquiries");
            DropForeignKey("dbo.Inquiries", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.RecievedCalls", "CallerId", "dbo.Callers");
            DropForeignKey("dbo.RecievedCalls", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AspNetUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AspNetRoles", "Personnel_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "RingStiffnessId" });
            DropIndex("dbo.Products", new[] { "PipeDiameterId" });
            DropIndex("dbo.Products", new[] { "PipeProfileId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.InquiryItems", new[] { "ProductId" });
            DropIndex("dbo.InquiryItems", new[] { "InquiryId" });
            DropIndex("dbo.Inquiries", new[] { "CustomerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", new[] { "Personnel_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.RecievedCalls", new[] { "DepartmentId" });
            DropIndex("dbo.RecievedCalls", new[] { "CallerId" });
            DropTable("dbo.RingStiffnesses");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.PipeProfiles");
            DropTable("dbo.PipeDiameters");
            DropTable("dbo.Products");
            DropTable("dbo.InquiryItems");
            DropTable("dbo.Inquiries");
            DropTable("dbo.Customers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Departments");
            DropTable("dbo.RecievedCalls");
            DropTable("dbo.Callers");
        }
    }
}
