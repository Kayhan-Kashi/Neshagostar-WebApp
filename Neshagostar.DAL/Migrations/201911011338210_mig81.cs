namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig81 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PersonnelId = c.String(nullable: false, maxLength: 128),
                        ControllerName = c.String(),
                        ActionMethodName = c.String(),
                        ActivityName = c.String(),
                        ModelNameBeingOperated = c.String(),
                        PersonnelName = c.String(),
                        DateTime = c.String(),
                        Url = c.String(),
                        DataOperatedInJson = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PersonnelId, cascadeDelete: true)
                .Index(t => t.PersonnelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityLogs", "PersonnelId", "dbo.AspNetUsers");
            DropIndex("dbo.ActivityLogs", new[] { "PersonnelId" });
            DropTable("dbo.ActivityLogs");
        }
    }
}
