namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "SearchCode", c => c.String());
            AddColumn("dbo.PMSPlanVHPHistories", "SearchCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSPlanVHPHistories", "SearchCode");
            DropColumn("dbo.PMSPlanVHPs", "SearchCode");
        }
    }
}
