namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change44 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "ProductionIndex", c => c.Double(nullable: false));
            AddColumn("dbo.PMSOrderHistories", "MaterialIndex", c => c.Double(nullable: false));
            AddColumn("dbo.PMSOrders", "MaterialIndex", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "MaterialIndex");
            DropColumn("dbo.PMSOrderHistories", "MaterialIndex");
            DropColumn("dbo.PMSOrderHistories", "ProductionIndex");
        }
    }
}
