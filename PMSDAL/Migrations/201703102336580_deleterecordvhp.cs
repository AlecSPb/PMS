namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleterecordvhp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecordVHPItems", "RecordVHPID", "dbo.RecordVHPs");
            DropIndex("dbo.RecordVHPItems", new[] { "RecordVHPID" });
            AddColumn("dbo.RecordVHPs", "CurrentTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordVHPs", "PV1", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "PV2", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "PV3", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "SV", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "Ton", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "Shift1", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "Shift2", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "Omega", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "WaterTemperatureOut", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "WaterTemperatureIn", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "ExtraInformation", c => c.String());
            AddColumn("dbo.RecordVHPs", "PlanVHPID", c => c.Guid());
            DropColumn("dbo.RecordVHPs", "CreateTime");
            DropColumn("dbo.RecordVHPs", "PlanID");
            DropColumn("dbo.RecordVHPs", "PlanDate");
            DropColumn("dbo.RecordVHPs", "Composition");
            DropColumn("dbo.RecordVHPs", "MoldType");
            DropColumn("dbo.RecordVHPs", "VHPDeviceCode");
            DropColumn("dbo.RecordVHPs", "MoldDiameter");
            DropColumn("dbo.RecordVHPs", "PreTemperature");
            DropColumn("dbo.RecordVHPs", "PrePressure");
            DropColumn("dbo.RecordVHPs", "Temperature");
            DropColumn("dbo.RecordVHPs", "Pressure");
            DropColumn("dbo.RecordVHPs", "KeepTempTime");
            DropColumn("dbo.RecordVHPs", "Remark");
            DropTable("dbo.RecordVHPItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecordVHPItems",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CurrentTime = c.DateTime(nullable: false),
                        State = c.String(),
                        PV1 = c.Double(nullable: false),
                        PV2 = c.Double(nullable: false),
                        PV3 = c.Double(nullable: false),
                        SV = c.Double(nullable: false),
                        Ton = c.Double(nullable: false),
                        Vaccum = c.Double(nullable: false),
                        Shift1 = c.Double(nullable: false),
                        Shift2 = c.Double(nullable: false),
                        Omega = c.Double(nullable: false),
                        WaterTemperatureOut = c.Double(nullable: false),
                        WaterTemperatureIn = c.Double(nullable: false),
                        ExtraInformation = c.String(),
                        RecordVHPID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RecordVHPs", "Remark", c => c.String());
            AddColumn("dbo.RecordVHPs", "KeepTempTime", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "Pressure", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "Temperature", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "PrePressure", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "PreTemperature", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "MoldDiameter", c => c.Double(nullable: false));
            AddColumn("dbo.RecordVHPs", "VHPDeviceCode", c => c.String());
            AddColumn("dbo.RecordVHPs", "MoldType", c => c.String());
            AddColumn("dbo.RecordVHPs", "Composition", c => c.String());
            AddColumn("dbo.RecordVHPs", "PlanDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordVHPs", "PlanID", c => c.Guid(nullable: false));
            AddColumn("dbo.RecordVHPs", "CreateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.RecordVHPs", "PlanVHPID");
            DropColumn("dbo.RecordVHPs", "ExtraInformation");
            DropColumn("dbo.RecordVHPs", "WaterTemperatureIn");
            DropColumn("dbo.RecordVHPs", "WaterTemperatureOut");
            DropColumn("dbo.RecordVHPs", "Omega");
            DropColumn("dbo.RecordVHPs", "Shift2");
            DropColumn("dbo.RecordVHPs", "Shift1");
            DropColumn("dbo.RecordVHPs", "Ton");
            DropColumn("dbo.RecordVHPs", "SV");
            DropColumn("dbo.RecordVHPs", "PV3");
            DropColumn("dbo.RecordVHPs", "PV2");
            DropColumn("dbo.RecordVHPs", "PV1");
            DropColumn("dbo.RecordVHPs", "CurrentTime");
            CreateIndex("dbo.RecordVHPItems", "RecordVHPID");
            AddForeignKey("dbo.RecordVHPItems", "RecordVHPID", "dbo.RecordVHPs", "ID");
        }
    }
}
