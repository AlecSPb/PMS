namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlateHistories", "UseCount", c => c.String());
            AddColumn("dbo.PlateHistories", "Hardness", c => c.String());
            AddColumn("dbo.PlateHistories", "LastWeldMaterial", c => c.String());
            AddColumn("dbo.Plates", "UseCount", c => c.String());
            AddColumn("dbo.Plates", "Hardness", c => c.String());
            AddColumn("dbo.Plates", "LastWeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetPMINumber", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPMINumber", c => c.String());
            DropColumn("dbo.RecordBondingHistories", "PMINumber");
            DropColumn("dbo.RecordBondings", "PMINumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordBondings", "PMINumber", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PMINumber", c => c.String());
            DropColumn("dbo.RecordBondings", "TargetPMINumber");
            DropColumn("dbo.RecordBondingHistories", "TargetPMINumber");
            DropColumn("dbo.Plates", "LastWeldMaterial");
            DropColumn("dbo.Plates", "Hardness");
            DropColumn("dbo.Plates", "UseCount");
            DropColumn("dbo.PlateHistories", "LastWeldMaterial");
            DropColumn("dbo.PlateHistories", "Hardness");
            DropColumn("dbo.PlateHistories", "UseCount");
        }
    }
}
