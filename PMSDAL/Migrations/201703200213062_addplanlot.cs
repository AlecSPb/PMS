namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addplanlot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "PlanLot", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSPlanVHPs", "PlanLot");
        }
    }
}
