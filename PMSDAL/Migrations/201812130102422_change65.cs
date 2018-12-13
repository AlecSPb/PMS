namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change65 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryItemHistories", "OrderNumber", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryItems", "OrderNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryItems", "OrderNumber");
            DropColumn("dbo.DeliveryItemHistories", "OrderNumber");
        }
    }
}
