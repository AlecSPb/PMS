namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recordvhp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVHPs", "MoldType", c => c.String());
            AddColumn("dbo.RecordVHPs", "VHPDeviceCode", c => c.String());
            AddColumn("dbo.RecordVHPs", "MoldDiameter", c => c.String());
            DropColumn("dbo.RecordVHPs", "MoldCode");
            DropColumn("dbo.RecordVHPs", "DeviceCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordVHPs", "DeviceCode", c => c.String());
            AddColumn("dbo.RecordVHPs", "MoldCode", c => c.String());
            DropColumn("dbo.RecordVHPs", "MoldDiameter");
            DropColumn("dbo.RecordVHPs", "VHPDeviceCode");
            DropColumn("dbo.RecordVHPs", "MoldType");
        }
    }
}
