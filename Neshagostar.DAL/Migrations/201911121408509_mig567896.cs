namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig567896 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "DateOfDispatch", c => c.String());
            AddColumn("dbo.Orders", "Comments", c => c.String());
            DropColumn("dbo.OrderItems", "DateOfDisptach");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "DateOfDisptach", c => c.String());
            DropColumn("dbo.Orders", "Comments");
            DropColumn("dbo.OrderItems", "DateOfDispatch");
        }
    }
}
