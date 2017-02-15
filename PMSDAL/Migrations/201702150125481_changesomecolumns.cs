namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesomecolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "MoldType", c => c.String());
            AddColumn("dbo.BDVHPMolds", "MoldType", c => c.String());
            AlterColumn("dbo.PMSPlanVHPs", "CalculationDensity", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "MoldDiameter", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Thickness", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "RoomTemperature", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "RoomHumidity", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "PreTemperature", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "PrePressure", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Temperature", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Pressure", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Vaccum", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "KeepTempTime", c => c.Double(nullable: false));
            DropColumn("dbo.PMSPlanVHPs", "CurrentMold");
            DropColumn("dbo.PMSPlanVHPs", "PowderWeight");
            DropColumn("dbo.BDVHPMolds", "ModelType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BDVHPMolds", "ModelType", c => c.String());
            AddColumn("dbo.PMSPlanVHPs", "PowderWeight", c => c.Double());
            AddColumn("dbo.PMSPlanVHPs", "CurrentMold", c => c.String());
            AlterColumn("dbo.PMSPlanVHPs", "KeepTempTime", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Vaccum", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Pressure", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Temperature", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "PrePressure", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "PreTemperature", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "RoomHumidity", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "RoomTemperature", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Thickness", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "MoldDiameter", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "CalculationDensity", c => c.Double());
            DropColumn("dbo.BDVHPMolds", "MoldType");
            DropColumn("dbo.PMSPlanVHPs", "MoldType");
        }
    }
}
