namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondingHistories", "PMINumber", c => c.String());
            AddColumn("dbo.RecordBondings", "PMINumber", c => c.String());
            AddColumn("dbo.RecordDeMoldHistories", "PMINumber", c => c.String());
            AddColumn("dbo.RecordDeMoldHistories", "Dimension", c => c.String());
            AddColumn("dbo.RecordDeMolds", "PMINumber", c => c.String());
            AddColumn("dbo.RecordDeMolds", "Dimension", c => c.String());
            AddColumn("dbo.RecordMachineHistories", "PMINumber", c => c.String());
            AddColumn("dbo.RecordMachineHistories", "BlankDimension", c => c.String());
            AddColumn("dbo.RecordMachines", "PMINumber", c => c.String());
            AddColumn("dbo.RecordMachines", "BlankDimension", c => c.String());
            AddColumn("dbo.RecordMillingHistories", "RoomTemperature", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillingHistories", "RoomHumidity", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillingHistories", "PMINumber", c => c.String());
            AddColumn("dbo.RecordMillings", "RoomTemperature", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "RoomHumidity", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "PMINumber", c => c.String());
            AddColumn("dbo.RecordTestHistories", "PMINumber", c => c.String());
            AddColumn("dbo.RecordTests", "PMINumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "PMINumber");
            DropColumn("dbo.RecordTestHistories", "PMINumber");
            DropColumn("dbo.RecordMillings", "PMINumber");
            DropColumn("dbo.RecordMillings", "RoomHumidity");
            DropColumn("dbo.RecordMillings", "RoomTemperature");
            DropColumn("dbo.RecordMillingHistories", "PMINumber");
            DropColumn("dbo.RecordMillingHistories", "RoomHumidity");
            DropColumn("dbo.RecordMillingHistories", "RoomTemperature");
            DropColumn("dbo.RecordMachines", "BlankDimension");
            DropColumn("dbo.RecordMachines", "PMINumber");
            DropColumn("dbo.RecordMachineHistories", "BlankDimension");
            DropColumn("dbo.RecordMachineHistories", "PMINumber");
            DropColumn("dbo.RecordDeMolds", "Dimension");
            DropColumn("dbo.RecordDeMolds", "PMINumber");
            DropColumn("dbo.RecordDeMoldHistories", "Dimension");
            DropColumn("dbo.RecordDeMoldHistories", "PMINumber");
            DropColumn("dbo.RecordBondings", "PMINumber");
            DropColumn("dbo.RecordBondingHistories", "PMINumber");
        }
    }
}
