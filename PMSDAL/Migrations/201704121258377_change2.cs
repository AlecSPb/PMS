namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "SingleWeight", c => c.Double(nullable: false));
            AddColumn("dbo.PMSPlanVHPs", "AllWeight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSPlanVHPs", "AllWeight");
            DropColumn("dbo.PMSPlanVHPs", "SingleWeight");
        }
    }
}
