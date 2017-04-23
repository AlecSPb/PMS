namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PMSPlanVHPHistories", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PMSPlanVHPHistories", "Remark", c => c.DateTime(nullable: false));
        }
    }
}
