namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig85639 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "Title", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tenders", "Title");
        }
    }
}
