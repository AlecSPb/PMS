namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PMSMaterialInventoryIns", newName: "MaterialInventoryIns");
            RenameTable(name: "dbo.PMSMaterialNeeds", newName: "MaterialNeeds");
            RenameTable(name: "dbo.PMSMaterialOrderItems", newName: "MaterialOrderItems");
            RenameTable(name: "dbo.PMSMaterialOrders", newName: "MaterialOrders");
            RenameTable(name: "dbo.RecordDeliveryItems", newName: "DeliveryItems");
            RenameTable(name: "dbo.RecordDeliveries", newName: "Deliveries");
            DropForeignKey("dbo.RecordDeliveryItems", "DeliveryID", "dbo.RecordDeliveries");
            DropIndex("dbo.DeliveryItems", new[] { "DeliveryID" });
            CreateTable(
                "dbo.MaterialInventoryOuts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        State = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        MaterialLot = c.String(),
                        PMINumber = c.String(),
                        Composition = c.String(),
                        Purity = c.String(),
                        Receiver = c.String(),
                        Weight = c.Double(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        ProductType = c.String(),
                        ProductID = c.String(),
                        Composition = c.String(),
                        Abbr = c.String(),
                        Customer = c.String(),
                        PO = c.String(),
                        Weight = c.String(),
                        DetailRecord = c.String(),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RecordBondings", "TargetLot", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetComposition", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetDimension", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetAppearance", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetThicknessCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetDiameterCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetCheckPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "PlateLot", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateSerialNumber", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateBelong", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateDimension", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateUseCount", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateHardness", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateSuplier", c => c.String());
            AddColumn("dbo.RecordBondings", "LastWeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "OtherRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateAppearance", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateCheckPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "TargetProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "CuStringDiameter", c => c.Double(nullable: false));
            AddColumn("dbo.RecordBondings", "BondWarpageFix", c => c.String());
            AddColumn("dbo.RecordBondings", "BondDimensionCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondCleanCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondAppearanceCheck", c => c.String());
            AddColumn("dbo.DeliveryItems", "PackNumber", c => c.Int(nullable: false));
            AddColumn("dbo.RecordDeMolds", "DeMoldType", c => c.String());
            AddColumn("dbo.RecordMillings", "Ratio", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "MillingTime", c => c.String());
            AlterColumn("dbo.DeliveryItems", "DeliveryID", c => c.Guid(nullable: false));
            DropColumn("dbo.RecordBondings", "Remark");
            DropColumn("dbo.RecordBondings", "Temperature");
            DropColumn("dbo.RecordBondings", "HeatRecord");
            DropTable("dbo.PMSMaterialInventoryOuts");
            DropTable("dbo.RecordBondingPlates");
            DropTable("dbo.RecordBondingTargets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecordBondingTargets",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
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
            
            CreateTable(
                "dbo.RecordBondingPlates",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        PlateMaterial = c.String(),
                        PlateLot = c.String(),
                        PlateSerialNumber = c.String(),
                        PlateBelong = c.String(),
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
                "dbo.PMSMaterialInventoryOuts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        State = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        MaterialLot = c.String(),
                        PMINumber = c.String(),
                        Composition = c.String(),
                        Purity = c.String(),
                        Receiver = c.String(),
                        Weight = c.Double(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RecordBondings", "HeatRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "Temperature", c => c.Double(nullable: false));
            AddColumn("dbo.RecordBondings", "Remark", c => c.String());
            AlterColumn("dbo.DeliveryItems", "DeliveryID", c => c.Guid());
            DropColumn("dbo.RecordMillings", "MillingTime");
            DropColumn("dbo.RecordMillings", "Ratio");
            DropColumn("dbo.RecordDeMolds", "DeMoldType");
            DropColumn("dbo.DeliveryItems", "PackNumber");
            DropColumn("dbo.RecordBondings", "BondAppearanceCheck");
            DropColumn("dbo.RecordBondings", "BondCleanCheck");
            DropColumn("dbo.RecordBondings", "BondCheck");
            DropColumn("dbo.RecordBondings", "BondWarpageCheck");
            DropColumn("dbo.RecordBondings", "BondDimensionCheck");
            DropColumn("dbo.RecordBondings", "BondWarpageFix");
            DropColumn("dbo.RecordBondings", "CuStringDiameter");
            DropColumn("dbo.RecordBondings", "WeldMaterial");
            DropColumn("dbo.RecordBondings", "PlateProcessRecord");
            DropColumn("dbo.RecordBondings", "TargetProcessRecord");
            DropColumn("dbo.RecordBondings", "PlateCheckTime");
            DropColumn("dbo.RecordBondings", "PlateCheckPerson");
            DropColumn("dbo.RecordBondings", "PlateAppearance");
            DropColumn("dbo.RecordBondings", "OtherRecord");
            DropColumn("dbo.RecordBondings", "LastWeldMaterial");
            DropColumn("dbo.RecordBondings", "PlateSuplier");
            DropColumn("dbo.RecordBondings", "PlateHardness");
            DropColumn("dbo.RecordBondings", "PlateUseCount");
            DropColumn("dbo.RecordBondings", "PlateDimension");
            DropColumn("dbo.RecordBondings", "PlateBelong");
            DropColumn("dbo.RecordBondings", "PlateSerialNumber");
            DropColumn("dbo.RecordBondings", "PlateMaterial");
            DropColumn("dbo.RecordBondings", "PlateLot");
            DropColumn("dbo.RecordBondings", "TargetCheckTime");
            DropColumn("dbo.RecordBondings", "TargetCheckPerson");
            DropColumn("dbo.RecordBondings", "TargetDiameterCheck");
            DropColumn("dbo.RecordBondings", "TargetThicknessCheck");
            DropColumn("dbo.RecordBondings", "TargetWarpageCheck");
            DropColumn("dbo.RecordBondings", "TargetAppearance");
            DropColumn("dbo.RecordBondings", "TargetDimension");
            DropColumn("dbo.RecordBondings", "TargetComposition");
            DropColumn("dbo.RecordBondings", "TargetLot");
            DropTable("dbo.Products");
            DropTable("dbo.MaterialInventoryOuts");
            CreateIndex("dbo.DeliveryItems", "DeliveryID");
            AddForeignKey("dbo.RecordDeliveryItems", "DeliveryID", "dbo.RecordDeliveries", "ID");
            RenameTable(name: "dbo.Deliveries", newName: "RecordDeliveries");
            RenameTable(name: "dbo.DeliveryItems", newName: "RecordDeliveryItems");
            RenameTable(name: "dbo.MaterialOrders", newName: "PMSMaterialOrders");
            RenameTable(name: "dbo.MaterialOrderItems", newName: "PMSMaterialOrderItems");
            RenameTable(name: "dbo.MaterialNeeds", newName: "PMSMaterialNeeds");
            RenameTable(name: "dbo.MaterialInventoryIns", newName: "PMSMaterialInventoryIns");
        }
    }
}
