namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig84731 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "DispatchComments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "DispatchComments");
        }
    }
}
