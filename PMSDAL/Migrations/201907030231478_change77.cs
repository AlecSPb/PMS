namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change77 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "IsLocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.PMSPlanVHPHistories", "IsLocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSPlanVHPHistories", "IsLocked");
            DropColumn("dbo.PMSPlanVHPs", "IsLocked");
        }
    }
}
