namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedeliveryentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DeliveryItems", newName: "RecordDeliveryItems");
            RenameTable(name: "dbo.Deliveries", newName: "RecordDeliveries");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RecordDeliveries", newName: "Deliveries");
            RenameTable(name: "dbo.RecordDeliveryItems", newName: "DeliveryItems");
        }
    }
}
