namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change138 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryItemHistories", "BondingPO", c => c.String());
            AddColumn("dbo.DeliveryItemHistories", "TrackingHistory", c => c.String());
            AddColumn("dbo.DeliveryItemHistories", "TCBRemark", c => c.String());
            AddColumn("dbo.DeliveryItemHistories", "TCBState", c => c.String());
            AddColumn("dbo.DeliveryItems", "BondingPO", c => c.String());
            AddColumn("dbo.DeliveryItems", "TrackingHistory", c => c.String());
            AddColumn("dbo.DeliveryItems", "TCBRemark", c => c.String());
            AddColumn("dbo.DeliveryItems", "TCBState", c => c.String());
            DropTable("dbo.DeliveryItemTCBs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DeliveryItemTCBs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        ProductType = c.String(),
                        ProductID = c.String(),
                        Composition = c.String(),
                        Abbr = c.String(),
                        Customer = c.String(),
                        PO = c.String(),
                        Weight = c.String(),
                        DetailRecord = c.String(),
                        Dimension = c.String(),
                        DimensionActual = c.String(),
                        Defects = c.String(),
                        Remark = c.String(),
                        DeliveryID = c.Guid(nullable: false),
                        BondingPO = c.String(),
                        TrackingHistory = c.String(),
                        TCBRemark = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.DeliveryItems", "TCBState");
            DropColumn("dbo.DeliveryItems", "TCBRemark");
            DropColumn("dbo.DeliveryItems", "TrackingHistory");
            DropColumn("dbo.DeliveryItems", "BondingPO");
            DropColumn("dbo.DeliveryItemHistories", "TCBState");
            DropColumn("dbo.DeliveryItemHistories", "TCBRemark");
            DropColumn("dbo.DeliveryItemHistories", "TrackingHistory");
            DropColumn("dbo.DeliveryItemHistories", "BondingPO");
        }
    }
}
