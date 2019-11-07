namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mih8362 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "InquiryId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "InquiryId");
        }
    }
}
