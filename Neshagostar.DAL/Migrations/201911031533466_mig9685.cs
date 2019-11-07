namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9685 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "IsWinner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tenders", "IsWinner");
        }
    }
}
