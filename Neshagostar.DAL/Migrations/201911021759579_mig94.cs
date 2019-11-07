namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig94 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "PredictionPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Tenders", "ParticipatingGuarantyPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tenders", "ParticipatingGuarantyPrice");
            DropColumn("dbo.Tenders", "PredictionPrice");
        }
    }
}
