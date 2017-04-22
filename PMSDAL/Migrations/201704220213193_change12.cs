namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "PlanType", c => c.String());
            AddColumn("dbo.PMSPlanVHPHistories", "PlanType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSPlanVHPHistories", "PlanType");
            DropColumn("dbo.PMSPlanVHPs", "PlanType");
        }
    }
}
