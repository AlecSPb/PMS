namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change137 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryHistories", "Receiver", c => c.String());
            AddColumn("dbo.DeliveryItemTCBs", "DeliveryID", c => c.Guid(nullable: false));
            AddColumn("dbo.DeliveryItemTCBs", "TCBRemark", c => c.String());
            DropColumn("dbo.DeliveryItemTCBs", "DeliveryName");
            DropColumn("dbo.DeliveryItemTCBs", "ExpressName");
            DropColumn("dbo.DeliveryItemTCBs", "ExpressNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryItemTCBs", "ExpressNumber", c => c.String());
            AddColumn("dbo.DeliveryItemTCBs", "ExpressName", c => c.String());
            AddColumn("dbo.DeliveryItemTCBs", "DeliveryName", c => c.String());
            DropColumn("dbo.DeliveryItemTCBs", "TCBRemark");
            DropColumn("dbo.DeliveryItemTCBs", "DeliveryID");
            DropColumn("dbo.DeliveryHistories", "Receiver");
        }
    }
}
