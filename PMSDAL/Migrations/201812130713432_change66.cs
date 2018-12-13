namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change66 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "Drawing", c => c.String());
            AddColumn("dbo.PMSOrderHistories", "SampleForAnlysis", c => c.String());
            AddColumn("dbo.PMSOrderHistories", "ShipTo", c => c.String());
            AddColumn("dbo.PMSOrderHistories", "WithBackingPlate", c => c.String());
            AddColumn("dbo.PMSOrders", "Drawing", c => c.String());
            AddColumn("dbo.PMSOrders", "SampleForAnlysis", c => c.String());
            AddColumn("dbo.PMSOrders", "ShipTo", c => c.String());
            AddColumn("dbo.PMSOrders", "WithBackingPlate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "WithBackingPlate");
            DropColumn("dbo.PMSOrders", "ShipTo");
            DropColumn("dbo.PMSOrders", "SampleForAnlysis");
            DropColumn("dbo.PMSOrders", "Drawing");
            DropColumn("dbo.PMSOrderHistories", "WithBackingPlate");
            DropColumn("dbo.PMSOrderHistories", "ShipTo");
            DropColumn("dbo.PMSOrderHistories", "SampleForAnlysis");
            DropColumn("dbo.PMSOrderHistories", "Drawing");
        }
    }
}
