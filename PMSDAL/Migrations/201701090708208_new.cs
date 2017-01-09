namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMSAccesses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AccessName = c.String(),
                        AccessCode = c.String(),
                        State = c.Int(nullable: false),
                        ExtraInformation = c.String(),
                        PMSRole_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PMSRoles", t => t.PMSRole_ID)
                .Index(t => t.PMSRole_ID);
            
            CreateTable(
                "dbo.Compounds",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        MaterialName = c.String(),
                        Density = c.Double(nullable: false),
                        MeltingPoint = c.String(),
                        BoilingPoint = c.String(),
                        SpecialProperty = c.String(),
                        InformationSource = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CustomerName = c.String(),
                        Address = c.String(),
                        ContactPerson = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        Memo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Receiver = c.String(),
                        Company = c.String(),
                        Country = c.String(),
                        Tax = c.String(),
                        Phone = c.String(),
                        PostCode = c.String(),
                        Email = c.String(),
                        CellPhone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeliveryItems",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductType = c.String(),
                        ProductID = c.String(),
                        Composition = c.String(),
                        Customer = c.String(),
                        PO = c.String(),
                        DetailRecord = c.String(),
                        Position = c.String(),
                        Remark = c.String(),
                        DeliveryID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Deliveries", t => t.DeliveryID)
                .Index(t => t.DeliveryID);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        InvoiceNumber = c.String(),
                        DeliveryName = c.String(),
                        DeliveryNumber = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        ShipTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        PackageType = c.String(),
                        PackageInformation = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSOrders",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CustomerName = c.String(),
                        PO = c.String(),
                        PMIWorkingNumber = c.String(),
                        CompositionStandard = c.String(),
                        CompositionOriginal = c.String(),
                        CompositoinAbbr = c.String(),
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
                        Priority = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        StateRemark = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        ReviewPassed = c.Boolean(nullable: false),
                        Reviewer = c.String(),
                        ReviewDate = c.DateTime(nullable: false),
                        PolicyType = c.String(),
                        PolicyContent = c.String(),
                        PolicyMaker = c.String(),
                        PolicyMakeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSProducts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
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
                        Remark = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordMachines",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Dimension = c.String(),
                        ExtraRequirement = c.String(),
                        Diameter1 = c.Double(nullable: false),
                        Diameter2 = c.Double(nullable: false),
                        Thickness1 = c.Double(nullable: false),
                        Thickness2 = c.Double(nullable: false),
                        Thickness3 = c.Double(nullable: false),
                        Thickness4 = c.Double(nullable: false),
                        Defects = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordMillings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RawMaterial = c.String(),
                        FromWho = c.String(),
                        ExtraInformation = c.String(),
                        MillingTool = c.String(),
                        GasProtection = c.String(),
                        MaterialIn = c.Double(nullable: false),
                        MaterialOut = c.Double(nullable: false),
                        MaterialRemain = c.Double(nullable: false),
                        CurrentState = c.Int(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordTakeOuts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CurrentTargetData = c.String(),
                        MoveOutTemperature = c.String(),
                        TakeOutTemperature = c.String(),
                        ExtraInformation = c.String(),
                        RoughTargetWeight = c.Double(nullable: false),
                        Diameter1 = c.Double(nullable: false),
                        Diameter2 = c.Double(nullable: false),
                        Thickness1 = c.Double(nullable: false),
                        Thickness2 = c.Double(nullable: false),
                        Thickness3 = c.Double(nullable: false),
                        Thickness4 = c.Double(nullable: false),
                        WithExtraThickness = c.String(),
                        CurrentState = c.Int(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecordVHPItems",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CurrentTime = c.DateTime(nullable: false),
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RecordVHPs", t => t.RecordVHPID)
                .Index(t => t.RecordVHPID);
            
            CreateTable(
                "dbo.RecordVHPs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.String(),
                        Creator = c.String(),
                        VHPID = c.String(),
                        DeivceCode = c.String(),
                        PreTemperature = c.Double(nullable: false),
                        PrePressure = c.Double(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Pressure = c.Double(nullable: false),
                        Vaccum = c.Double(nullable: false),
                        KeepTempTime = c.Double(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSRoles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        GroupName = c.String(),
                        ExtraInformation = c.String(),
                        State = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSSamples",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductID = c.String(),
                        Composition = c.String(),
                        CompositionAbbr = c.String(),
                        PO = c.String(),
                        Customer = c.String(),
                        Weight1 = c.String(),
                        Weight2 = c.String(),
                        Weight3 = c.String(),
                        Weight4 = c.String(),
                        ForTarget = c.String(),
                        Remark = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSUsers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        RealName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Role_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PMSRoles", t => t.Role_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.VHPDevices",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CodeName = c.String(),
                        DeviceInformation = c.String(),
                        HighestTemperature = c.Double(nullable: false),
                        HighestPressure = c.Double(nullable: false),
                        HighestDiameter = c.Double(nullable: false),
                        State = c.Int(nullable: false),
                        Manufacturer = c.String(),
                        ReceiveTime = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VHPMolds",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ModelType = c.String(),
                        MoldDetails = c.String(),
                        InnerDiameter = c.Double(nullable: false),
                        ModelHeight = c.Double(nullable: false),
                        State = c.Int(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMSPlanVHPs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        OrderID = c.Guid(nullable: false),
                        PlanDate = c.DateTime(nullable: false),
                        VHPDeviceCode = c.String(),
                        CurrentMold = c.String(),
                        FillRequirement = c.String(),
                        CalculationDensity = c.Double(nullable: false),
                        MoldDiameter = c.Double(nullable: false),
                        Thickness = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PowderWeight = c.Double(nullable: false),
                        GrainSize = c.Double(nullable: false),
                        MillingRequirement = c.String(),
                        RoomTemperature = c.String(),
                        RoomHumidity = c.String(),
                        PreTemperature = c.Double(nullable: false),
                        PrePressure = c.Double(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Pressure = c.Double(nullable: false),
                        Vaccum = c.Double(nullable: false),
                        KeepTempTime = c.Double(nullable: false),
                        SpecialRequirement = c.String(),
                        FillingRequirement = c.String(),
                        LaterProcess = c.String(),
                        LaterProcessDetails = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VHPProcesses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CodeName = c.String(),
                        CodeMeaning = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PMSUsers", "Role_ID", "dbo.PMSRoles");
            DropForeignKey("dbo.PMSAccesses", "PMSRole_ID", "dbo.PMSRoles");
            DropForeignKey("dbo.RecordVHPItems", "RecordVHPID", "dbo.RecordVHPs");
            DropForeignKey("dbo.DeliveryItems", "DeliveryID", "dbo.Deliveries");
            DropIndex("dbo.PMSUsers", new[] { "Role_ID" });
            DropIndex("dbo.RecordVHPItems", new[] { "RecordVHPID" });
            DropIndex("dbo.DeliveryItems", new[] { "DeliveryID" });
            DropIndex("dbo.PMSAccesses", new[] { "PMSRole_ID" });
            DropTable("dbo.VHPProcesses");
            DropTable("dbo.PMSPlanVHPs");
            DropTable("dbo.VHPMolds");
            DropTable("dbo.VHPDevices");
            DropTable("dbo.PMSUsers");
            DropTable("dbo.PMSSamples");
            DropTable("dbo.PMSRoles");
            DropTable("dbo.RecordVHPs");
            DropTable("dbo.RecordVHPItems");
            DropTable("dbo.RecordTakeOuts");
            DropTable("dbo.RecordMillings");
            DropTable("dbo.RecordMachines");
            DropTable("dbo.PMSProducts");
            DropTable("dbo.PMSOrders");
            DropTable("dbo.Deliveries");
            DropTable("dbo.DeliveryItems");
            DropTable("dbo.DeliveryAddresses");
            DropTable("dbo.Customers");
            DropTable("dbo.Compounds");
            DropTable("dbo.PMSAccesses");
        }
    }
}
