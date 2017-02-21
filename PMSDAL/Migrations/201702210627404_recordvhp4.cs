namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recordvhp4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PMSPlanVHPs", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PMSPlanVHPs", "CreateTime", c => c.DateTime());
        }
    }
}
