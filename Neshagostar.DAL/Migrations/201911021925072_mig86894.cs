namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig86894 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tenders", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tenders", "Title", c => c.Double(nullable: false));
        }
    }
}
