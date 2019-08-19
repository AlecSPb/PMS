namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change80 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaintenanceRecords", "Device", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "Part", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "Persons", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "Content", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "Remark", c => c.String());
            DropColumn("dbo.MaintenanceRecords", "PlanID");
            DropColumn("dbo.MaintenanceRecords", "MaintenancePersons");
            DropColumn("dbo.MaintenanceRecords", "MaintenanceContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaintenanceRecords", "MaintenanceContent", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "MaintenancePersons", c => c.String());
            AddColumn("dbo.MaintenanceRecords", "PlanID", c => c.Guid(nullable: false));
            DropColumn("dbo.MaintenanceRecords", "Remark");
            DropColumn("dbo.MaintenanceRecords", "Content");
            DropColumn("dbo.MaintenanceRecords", "Persons");
            DropColumn("dbo.MaintenanceRecords", "Part");
            DropColumn("dbo.MaintenanceRecords", "Device");
        }
    }
}
