namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change133 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "BondingRequirement", c => c.String());
            AddColumn("dbo.PMSOrders", "BondingRequirement", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "BondingRequirement");
            DropColumn("dbo.PMSOrderHistories", "BondingRequirement");
        }
    }
}
