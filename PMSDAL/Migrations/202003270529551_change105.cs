namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change105 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryHistories", "PackageWeight", c => c.Double(nullable: false));
            AddColumn("dbo.DeliveryHistories", "CustomerSignedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryHistories", "CustomerSignedDetails", c => c.String());
            AddColumn("dbo.Deliveries", "LastCheckIDCollection", c => c.String());
            AddColumn("dbo.Deliveries", "PackageWeight", c => c.Double(nullable: false));
            AddColumn("dbo.Deliveries", "CustomerSignedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Deliveries", "CustomerSignedDetails", c => c.String());
            AddColumn("dbo.PMSOrderHistories", "PlateDrawing", c => c.String());
            AddColumn("dbo.PMSOrders", "PlateDrawing", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "PlateDrawing");
            DropColumn("dbo.PMSOrderHistories", "PlateDrawing");
            DropColumn("dbo.Deliveries", "CustomerSignedDetails");
            DropColumn("dbo.Deliveries", "CustomerSignedDate");
            DropColumn("dbo.Deliveries", "PackageWeight");
            DropColumn("dbo.Deliveries", "LastCheckIDCollection");
            DropColumn("dbo.DeliveryHistories", "CustomerSignedDetails");
            DropColumn("dbo.DeliveryHistories", "CustomerSignedDate");
            DropColumn("dbo.DeliveryHistories", "PackageWeight");
        }
    }
}
