namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change43 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrders", "ProductionIndex", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "ProductionIndex");
        }
    }
}
