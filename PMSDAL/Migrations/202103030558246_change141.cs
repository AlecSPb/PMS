namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change141 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "MoldNumber", c => c.String());
            AddColumn("dbo.PMSPlanVHPHistories", "MoldNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSPlanVHPHistories", "MoldNumber");
            DropColumn("dbo.PMSPlanVHPs", "MoldNumber");
        }
    }
}
