namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9372 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecievedCalls", "IsCallingFromOutside", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecievedCalls", "IsCallingFromOutside");
        }
    }
}
