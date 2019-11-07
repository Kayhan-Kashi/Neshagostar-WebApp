namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig853 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TenderItems", "CarriageCostPerKilo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TenderItems", "CarriageCostPerKilo", c => c.Double(nullable: false));
        }
    }
}
