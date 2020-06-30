namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change135 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryItemTCBs", "TrackingHistory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryItemTCBs", "TrackingHistory");
        }
    }
}
