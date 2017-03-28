namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrders", "PMINumber", c => c.String());
            AddColumn("dbo.PMSOrders", "CompositionAbbr", c => c.String());
            AlterColumn("dbo.PMSOrders", "Quantity", c => c.Double(nullable: false));
            DropColumn("dbo.PMSOrders", "PMIWorkingNumber");
            DropColumn("dbo.PMSOrders", "CompositoinAbbr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PMSOrders", "CompositoinAbbr", c => c.String());
            AddColumn("dbo.PMSOrders", "PMIWorkingNumber", c => c.String());
            AlterColumn("dbo.PMSOrders", "Quantity", c => c.Double());
            DropColumn("dbo.PMSOrders", "CompositionAbbr");
            DropColumn("dbo.PMSOrders", "PMINumber");
        }
    }
}
