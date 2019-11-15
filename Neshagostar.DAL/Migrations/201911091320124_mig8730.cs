namespace Neshagostar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig8730 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItemSendingDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderItemId = c.Guid(nullable: false),
                        DateTime = c.String(),
                        SendingAmount = c.Double(nullable: false),
                        CarrierNumberCode = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderItems", t => t.OrderItemId, cascadeDelete: true)
                .Index(t => t.OrderItemId);
            
            AddColumn("dbo.OrderItems", "DispatchDate", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItemSendingDetails", "OrderItemId", "dbo.OrderItems");
            DropIndex("dbo.OrderItemSendingDetails", new[] { "OrderItemId" });
            DropColumn("dbo.OrderItems", "DispatchDate");
            DropTable("dbo.OrderItemSendingDetails");
        }
    }
}
