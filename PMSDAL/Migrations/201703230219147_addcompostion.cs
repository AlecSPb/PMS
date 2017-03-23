namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcompostion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeMolds", "VHPPlanLot", c => c.String());
            AddColumn("dbo.RecordDeMolds", "Composition", c => c.String());
            AddColumn("dbo.RecordMachines", "VHPPlanLot", c => c.String());
            AddColumn("dbo.RecordMachines", "Composition", c => c.String());
            AddColumn("dbo.RecordMillings", "VHPPlanLot", c => c.String());
            DropColumn("dbo.RecordDeMolds", "PlanID");
            DropColumn("dbo.RecordMachines", "PlanID");
            DropColumn("dbo.RecordMillings", "PlanID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordMillings", "PlanID", c => c.Guid(nullable: false));
            AddColumn("dbo.RecordMachines", "PlanID", c => c.Guid(nullable: false));
            AddColumn("dbo.RecordDeMolds", "PlanID", c => c.Guid(nullable: false));
            DropColumn("dbo.RecordMillings", "VHPPlanLot");
            DropColumn("dbo.RecordMachines", "Composition");
            DropColumn("dbo.RecordMachines", "VHPPlanLot");
            DropColumn("dbo.RecordDeMolds", "Composition");
            DropColumn("dbo.RecordDeMolds", "VHPPlanLot");
        }
    }
}
