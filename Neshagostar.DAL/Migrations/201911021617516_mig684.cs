namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig684 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "GuarantyCost", c => c.Double(nullable: false));
            AddColumn("dbo.Tenders", "CarriageCost", c => c.Double(nullable: false));
            AddColumn("dbo.Tenders", "InspectionCost", c => c.Double(nullable: false));
            DropColumn("dbo.Tenders", "GuarantyPrice");
            DropColumn("dbo.Tenders", "AddedCostAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tenders", "AddedCostAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Tenders", "GuarantyPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Tenders", "InspectionCost");
            DropColumn("dbo.Tenders", "CarriageCost");
            DropColumn("dbo.Tenders", "GuarantyCost");
        }
    }
}
