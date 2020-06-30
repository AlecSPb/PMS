namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change134 : DbMigration
    {
        public override void Up()
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
                        DeliveryName = c.String(),
                        ExpressName = c.String(),
                        ExpressNumber = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Deliveries", "Receiver", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "Receiver");
            DropTable("dbo.DeliveryItemTCBs");
        }
    }
}
