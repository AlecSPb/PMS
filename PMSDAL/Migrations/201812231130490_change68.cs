namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change68 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "SpecialRequirement", c => c.String());
            AddColumn("dbo.PMSOrders", "SpecialRequirement", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "SpecialRequirement");
            DropColumn("dbo.PMSOrderHistories", "SpecialRequirement");
        }
    }
}
