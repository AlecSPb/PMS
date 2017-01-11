namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullorderplan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PMSOrders", "Quantity", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "CreateTime", c => c.DateTime());
            AlterColumn("dbo.PMSPlanVHPs", "CalculationDensity", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "MoldDiameter", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Thickness", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "PowderWeight", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "RoomTemperature", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "RoomHumidity", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "PreTemperature", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "PrePressure", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Temperature", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Pressure", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "Vaccum", c => c.Double());
            AlterColumn("dbo.PMSPlanVHPs", "KeepTempTime", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PMSPlanVHPs", "KeepTempTime", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Vaccum", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Pressure", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Temperature", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "PrePressure", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "PreTemperature", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "RoomHumidity", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "RoomTemperature", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "PowderWeight", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "Thickness", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "MoldDiameter", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "CalculationDensity", c => c.Double(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PMSOrders", "Quantity", c => c.Double(nullable: false));
        }
    }
}
