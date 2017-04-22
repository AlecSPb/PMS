namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PMSPlanVHPs", "PlanType", c => c.String());
            AlterColumn("dbo.PMSPlanVHPHistories", "PlanType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PMSPlanVHPHistories", "PlanType", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "PlanType", c => c.Int(nullable: false));
        }
    }
}
