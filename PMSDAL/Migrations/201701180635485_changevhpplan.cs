namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changevhpplan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "ProcessCode", c => c.String());
            AddColumn("dbo.PMSPlanVHPs", "VHPRequirement", c => c.String());
            AddColumn("dbo.PMSPlanVHPs", "MachineRequirement", c => c.String());
            DropColumn("dbo.PMSPlanVHPs", "FillRequirement");
            DropColumn("dbo.PMSPlanVHPs", "LaterProcess");
            DropColumn("dbo.PMSPlanVHPs", "LaterProcessDetails");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PMSPlanVHPs", "LaterProcessDetails", c => c.String());
            AddColumn("dbo.PMSPlanVHPs", "LaterProcess", c => c.String());
            AddColumn("dbo.PMSPlanVHPs", "FillRequirement", c => c.String());
            DropColumn("dbo.PMSPlanVHPs", "MachineRequirement");
            DropColumn("dbo.PMSPlanVHPs", "VHPRequirement");
            DropColumn("dbo.PMSPlanVHPs", "ProcessCode");
        }
    }
}
