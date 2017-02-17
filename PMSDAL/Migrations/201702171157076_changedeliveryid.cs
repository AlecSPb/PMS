namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedeliveryid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeliveries", "DeliveryID", c => c.String());
            DropColumn("dbo.RecordDeliveries", "DeliveryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordDeliveries", "DeliveryName", c => c.String());
            DropColumn("dbo.RecordDeliveries", "DeliveryID");
        }
    }
}
