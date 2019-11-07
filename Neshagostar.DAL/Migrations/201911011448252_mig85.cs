namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig85 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityLogs", "ModelIdBeingOperated", c => c.String());
            DropColumn("dbo.ActivityLogs", "DataOperatedInJson");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityLogs", "DataOperatedInJson", c => c.String());
            DropColumn("dbo.ActivityLogs", "ModelIdBeingOperated");
        }
    }
}
