namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmoreitems : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MaitenancePlans", newName: "MaintenancePlans");
            RenameTable(name: "dbo.RecordTakeOuts", newName: "RecordDeMolds");
            AddColumn("dbo.BDVHPMolds", "MoldName", c => c.String());
            AddColumn("dbo.BDVHPMolds", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.BDVHPMolds", "EstimateUsedCount", c => c.Int(nullable: false));
            AddColumn("dbo.BDVHPMolds", "CurrentUsedCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BDVHPMolds", "CurrentUsedCount");
            DropColumn("dbo.BDVHPMolds", "EstimateUsedCount");
            DropColumn("dbo.BDVHPMolds", "StartTime");
            DropColumn("dbo.BDVHPMolds", "MoldName");
            RenameTable(name: "dbo.RecordDeMolds", newName: "RecordTakeOuts");
            RenameTable(name: "dbo.MaintenancePlans", newName: "MaitenancePlans");
        }
    }
}
