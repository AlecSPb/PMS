namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change140 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaintenancePlans", "VHPMachineCode", c => c.String());
            AddColumn("dbo.MaintenancePlans", "PlanType", c => c.String());
            AddColumn("dbo.MaintenancePlans", "PlanInterval", c => c.String());
            AddColumn("dbo.MaintenancePlans", "Content", c => c.String());
            AddColumn("dbo.MaintenancePlans", "Standard", c => c.String());
            AddColumn("dbo.MaintenancePlans", "CommonFailure", c => c.String());
            AddColumn("dbo.MaintenancePlans", "ProcessMethod", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "VHPMachineCode", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "PlanItem", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "PlanType", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "PlanInterval", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "Log", c => c.String());
            DropColumn("dbo.MaintenancePlans", "DeviceCode");
            DropColumn("dbo.MaintenancePlans", "IntervalCount");
            DropColumn("dbo.MaintenancePlans", "CurrentCount");
            DropColumn("dbo.MaintenanceRecords", "Device");
            DropColumn("dbo.MaintenanceRecords", "Part");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaintenanceRecords", "Part", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "Device", c => c.String());
            AddColumn("dbo.MaintenancePlans", "CurrentCount", c => c.Int(nullable: false));
            AddColumn("dbo.MaintenancePlans", "IntervalCount", c => c.Int(nullable: false));
            AddColumn("dbo.MaintenancePlans", "DeviceCode", c => c.String());
            DropColumn("dbo.MaintenanceRecords", "Log");
            DropColumn("dbo.MaintenanceRecords", "PlanInterval");
            DropColumn("dbo.MaintenanceRecords", "PlanType");
            DropColumn("dbo.MaintenanceRecords", "PlanItem");
            DropColumn("dbo.MaintenanceRecords", "VHPMachineCode");
            DropColumn("dbo.MaintenancePlans", "ProcessMethod");
            DropColumn("dbo.MaintenancePlans", "CommonFailure");
            DropColumn("dbo.MaintenancePlans", "Standard");
            DropColumn("dbo.MaintenancePlans", "Content");
            DropColumn("dbo.MaintenancePlans", "PlanInterval");
            DropColumn("dbo.MaintenancePlans", "PlanType");
            DropColumn("dbo.MaintenancePlans", "VHPMachineCode");
        }
    }
}
