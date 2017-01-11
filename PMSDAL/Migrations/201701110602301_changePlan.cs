namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "Remark", c => c.String());
            AlterColumn("dbo.PMSPlanVHPs", "GrainSize", c => c.String());
            AlterColumn("dbo.PMSPlanVHPs", "RoomTemperature", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "RoomHumidity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PMSPlanVHPs", "RoomHumidity", c => c.String());
            AlterColumn("dbo.PMSPlanVHPs", "RoomTemperature", c => c.String());
            AlterColumn("dbo.PMSPlanVHPs", "GrainSize", c => c.Double(nullable: false));
            DropColumn("dbo.PMSPlanVHPs", "Remark");
        }
    }
}
