namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newchange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaitenancePlans",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.String(),
                        State = c.String(),
                        DeviceCode = c.String(),
                        PlanItem = c.String(),
                        IntervalCount = c.Int(nullable: false),
                        CurrentCount = c.Int(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MaintenanceRecords",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.String(),
                        State = c.String(),
                        PlanID = c.Guid(nullable: false),
                        MaintenancePersons = c.String(),
                        MaintenanceContent = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordBondingPlates",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.String(),
                        State = c.String(),
                        PlateMaterial = c.String(),
                        PlateLot = c.String(),
                        PlateDimension = c.String(),
                        PlateUseCount = c.String(),
                        PlateHardness = c.String(),
                        PlateSuplier = c.String(),
                        LastWeldMaterial = c.String(),
                        OtherRecord = c.String(),
                        PlateAppearance = c.String(),
                        PlateProcessRecord = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordBondings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.String(),
                        State = c.String(),
                        InstructionCode = c.String(),
                        Remark = c.String(),
                        Temperature = c.Double(nullable: false),
                        HeatRecord = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordBondingTargets",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.String(),
                        State = c.String(),
                        BondingID = c.Guid(nullable: false),
                        PlateID = c.Guid(nullable: false),
                        TargetLot = c.String(),
                        TargetComposition = c.String(),
                        TargetDimension = c.String(),
                        TargetAppearance = c.String(),
                        TargetWarpageCheck = c.String(),
                        TargetThicknessCheck = c.String(),
                        TargetDiameterCheck = c.String(),
                        TargetProcessRecord = c.String(),
                        WeldMaterial = c.String(),
                        CuStringDiameter = c.Double(nullable: false),
                        BondWarpageFix = c.String(),
                        BondDimensionCheck = c.String(),
                        BondWarpageCheck = c.String(),
                        BondCheck = c.String(),
                        BondCleanCheck = c.String(),
                        BondAppearanceCheck = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RecordMachines", "Creator", c => c.String());
            AddColumn("dbo.RecordMachines", "CreateTime", c => c.String());
            AddColumn("dbo.RecordMachines", "State", c => c.String());
            AddColumn("dbo.RecordMachines", "PlanID", c => c.Guid(nullable: false));
            AddColumn("dbo.RecordMillings", "PlanID", c => c.Guid(nullable: false));
            AddColumn("dbo.RecordTakeOuts", "PlanID", c => c.Guid(nullable: false));
            AlterColumn("dbo.RecordMillings", "CreateTime", c => c.String());
            AlterColumn("dbo.RecordTakeOuts", "CreateTime", c => c.String());
            DropColumn("dbo.RecordTakeOuts", "CurrentTargetData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordTakeOuts", "CurrentTargetData", c => c.String());
            AlterColumn("dbo.RecordTakeOuts", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RecordMillings", "CreateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.RecordTakeOuts", "PlanID");
            DropColumn("dbo.RecordMillings", "PlanID");
            DropColumn("dbo.RecordMachines", "PlanID");
            DropColumn("dbo.RecordMachines", "State");
            DropColumn("dbo.RecordMachines", "CreateTime");
            DropColumn("dbo.RecordMachines", "Creator");
            DropTable("dbo.RecordBondingTargets");
            DropTable("dbo.RecordBondings");
            DropTable("dbo.RecordBondingPlates");
            DropTable("dbo.MaintenanceRecords");
            DropTable("dbo.MaitenancePlans");
        }
    }
}
