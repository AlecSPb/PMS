namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        DeliveryName = c.String(),
                        InvoiceNumber = c.String(),
                        DeliveryNumber = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        ShipTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        PackageType = c.String(),
                        PackageInformation = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeliveryItemHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        ProductType = c.String(),
                        ProductID = c.String(),
                        Composition = c.String(),
                        Abbr = c.String(),
                        Customer = c.String(),
                        PO = c.String(),
                        Weight = c.String(),
                        DetailRecord = c.String(),
                        Dimension = c.String(),
                        DimensionActual = c.String(),
                        Defects = c.String(),
                        PackNumber = c.Int(nullable: false),
                        Position = c.String(),
                        Remark = c.String(),
                        State = c.String(),
                        DeliveryID = c.Guid(nullable: false),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MaterialInventoryInHistories",
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
                        Supplier = c.String(),
                        Weight = c.Double(nullable: false),
                        Remark = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialInventoryOutHistories",
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
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialNeedHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        State = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Composition = c.String(),
                        Purity = c.String(),
                        Weight = c.Double(nullable: false),
                        SpecialNeeds = c.String(),
                        PMINumber = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialOrderHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        OrderPO = c.String(),
                        Supplier = c.String(),
                        SupplierAbbr = c.String(),
                        SupplierReceiver = c.String(),
                        SupplierEmail = c.String(),
                        SupplierAddress = c.String(),
                        Remark = c.String(),
                        ShipFee = c.Double(nullable: false),
                        Priority = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MaterialOrderItemHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        OrderItemNumber = c.String(),
                        PMINumber = c.String(),
                        Composition = c.String(),
                        Purity = c.String(),
                        Description = c.String(),
                        ProvideRawMaterial = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        MaterialOrderID = c.Guid(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSOrderHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CustomerName = c.String(),
                        PO = c.String(),
                        PMINumber = c.String(),
                        CompositionStandard = c.String(),
                        CompositionOriginal = c.String(),
                        CompositionAbbr = c.String(),
                        ProductType = c.String(),
                        Purity = c.String(),
                        Quantity = c.Double(nullable: false),
                        QuantityUnit = c.String(),
                        Dimension = c.String(),
                        DimensionDetails = c.String(),
                        SampleNeed = c.String(),
                        DeadLine = c.DateTime(nullable: false),
                        MinimumAcceptDefect = c.String(),
                        Remark = c.String(),
                        Priority = c.String(),
                        State = c.String(),
                        StateRemark = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        ReviewTime = c.DateTime(nullable: false),
                        Reviewer = c.String(),
                        PolicyType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductHistories",
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
                        Dimension = c.String(),
                        DimensionActual = c.String(),
                        Defects = c.String(),
                        DetailRecord = c.String(),
                        Position = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordBondingHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        InstructionCode = c.String(),
                        TargetProductID = c.String(),
                        TargetComposition = c.String(),
                        TargetAbbr = c.String(),
                        TargetCustomer = c.String(),
                        TargetPO = c.String(),
                        TargetWeight = c.String(),
                        TargetDimension = c.String(),
                        TargetDimensionActual = c.String(),
                        TargetDefects = c.String(),
                        TargetDetailRecord = c.String(),
                        TargetAppearance = c.String(),
                        TargetWarpageCheck = c.String(),
                        TargetThicknessCheck = c.String(),
                        TargetDiameterCheck = c.String(),
                        TargetPerson = c.String(),
                        TargetCheckTime = c.DateTime(nullable: false),
                        PlateLot = c.String(),
                        PlateMaterial = c.String(),
                        PlateBelong = c.String(),
                        PlateDimension = c.String(),
                        PlateUseCount = c.String(),
                        PlateHardness = c.String(),
                        PlateSuplier = c.String(),
                        PlateLastWeldMaterial = c.String(),
                        PlateOtherRecord = c.String(),
                        PlateAppearance = c.String(),
                        PlatePerson = c.String(),
                        PlateCheckTime = c.DateTime(nullable: false),
                        TargetPreProcessRecord = c.String(),
                        TargetPreProcessPerson = c.String(),
                        TargetPreProcessCheckTime = c.DateTime(nullable: false),
                        PlatePreProcessRecord = c.String(),
                        PlatePreProcessPerson = c.String(),
                        PlatePreProcessCheckTime = c.DateTime(nullable: false),
                        WeldMaterial = c.String(),
                        WeldCuStringDiameter = c.String(),
                        WeldHold = c.String(),
                        WeldPerson = c.String(),
                        WeldCheckTime = c.DateTime(nullable: false),
                        WarpageFix = c.String(),
                        WarpagePerson = c.String(),
                        WarpageCheckTime = c.DateTime(nullable: false),
                        DimensionCheck = c.String(),
                        DimensionWarpageCheck = c.String(),
                        DimensionPerson = c.String(),
                        DimensionCheckTime = c.DateTime(nullable: false),
                        BindingCheck = c.String(),
                        BindingPerson = c.String(),
                        BindingCheckTime = c.DateTime(nullable: false),
                        SprayCheck = c.String(),
                        SprayPerson = c.String(),
                        SprayCheckTime = c.DateTime(nullable: false),
                        CleanCheck = c.String(),
                        CleanPerson = c.String(),
                        CleanCheckTime = c.DateTime(nullable: false),
                        ApperanceCheck = c.String(),
                        ApperancePerson = c.String(),
                        ApperanceCheckTime = c.DateTime(nullable: false),
                        PackCheck = c.String(),
                        PackPerson = c.String(),
                        PackCheckTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordDeMoldHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        VHPPlanLot = c.String(),
                        Composition = c.String(),
                        Temperature1 = c.String(),
                        Temperature2 = c.String(),
                        DeMoldType = c.String(),
                        Weight = c.Double(nullable: false),
                        Diameter1 = c.Double(nullable: false),
                        Diameter2 = c.Double(nullable: false),
                        Thickness1 = c.Double(nullable: false),
                        Thickness2 = c.Double(nullable: false),
                        Thickness3 = c.Double(nullable: false),
                        Thickness4 = c.Double(nullable: false),
                        Remark = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordMachineHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        VHPPlanLot = c.String(),
                        Composition = c.String(),
                        Dimension = c.String(),
                        ExtraRequirement = c.String(),
                        Diameter1 = c.Double(nullable: false),
                        Diameter2 = c.Double(nullable: false),
                        Thickness1 = c.Double(nullable: false),
                        Thickness2 = c.Double(nullable: false),
                        Thickness3 = c.Double(nullable: false),
                        Thickness4 = c.Double(nullable: false),
                        Defects = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordMillingHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        Composition = c.String(),
                        VHPPlanLot = c.String(),
                        MaterialSource = c.String(),
                        Remark = c.String(),
                        MillingTool = c.String(),
                        GasProtection = c.String(),
                        WeightIn = c.Double(nullable: false),
                        WeightOut = c.Double(nullable: false),
                        WeightRemain = c.Double(nullable: false),
                        Ratio = c.Double(nullable: false),
                        MillingTime = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordTestHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        TestType = c.String(),
                        ProductID = c.String(),
                        Composition = c.String(),
                        CompositionAbbr = c.String(),
                        PO = c.String(),
                        Customer = c.String(),
                        Dimension = c.String(),
                        Density = c.String(),
                        Weight = c.String(),
                        Resistance = c.String(),
                        CompositionXRF = c.String(),
                        DimensionActual = c.String(),
                        Defects = c.String(),
                        Remark = c.String(),
                        Sample = c.String(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordVHPHistories",
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
                        PlanVHPID = c.Guid(),
                        Operator = c.String(),
                        OperateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSPlanVHPHistories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        State = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        OrderID = c.Guid(nullable: false),
                        PlanDate = c.DateTime(nullable: false),
                        VHPDeviceCode = c.String(),
                        PlanLot = c.Int(nullable: false),
                        MoldType = c.String(),
                        CalculationDensity = c.Double(nullable: false),
                        MoldDiameter = c.Double(nullable: false),
                        Thickness = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SingleWeight = c.Double(nullable: false),
                        AllWeight = c.Double(nullable: false),
                        GrainSize = c.String(),
                        RoomTemperature = c.Double(nullable: false),
                        RoomHumidity = c.Double(nullable: false),
                        PreTemperature = c.Double(nullable: false),
                        PrePressure = c.Double(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Pressure = c.Double(nullable: false),
                        Vaccum = c.Double(nullable: false),
                        KeepTempTime = c.Double(nullable: false),
                        ProcessCode = c.String(),
                        MillingRequirement = c.String(),
                        FillingRequirement = c.String(),
                        VHPRequirement = c.String(),
                        MachineRequirement = c.String(),
                        SpecialRequirement = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RecordBondings", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordBondings", "Remark");
            DropTable("dbo.PMSPlanVHPHistories");
            DropTable("dbo.RecordVHPHistories");
            DropTable("dbo.RecordTestHistories");
            DropTable("dbo.RecordMillingHistories");
            DropTable("dbo.RecordMachineHistories");
            DropTable("dbo.RecordDeMoldHistories");
            DropTable("dbo.RecordBondingHistories");
            DropTable("dbo.ProductHistories");
            DropTable("dbo.PMSOrderHistories");
            DropTable("dbo.MaterialOrderItemHistories");
            DropTable("dbo.MaterialOrderHistories");
            DropTable("dbo.MaterialNeedHistories");
            DropTable("dbo.MaterialInventoryOutHistories");
            DropTable("dbo.MaterialInventoryInHistories");
            DropTable("dbo.DeliveryItemHistories");
            DropTable("dbo.DeliveryHistories");
        }
    }
}
