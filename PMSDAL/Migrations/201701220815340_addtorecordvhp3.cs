namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtorecordvhp3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVHPs", "DeviceCode", c => c.String());
            DropColumn("dbo.RecordVHPs", "DeivceCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordVHPs", "DeivceCode", c => c.String());
            DropColumn("dbo.RecordVHPs", "DeviceCode");
        }
    }
}
