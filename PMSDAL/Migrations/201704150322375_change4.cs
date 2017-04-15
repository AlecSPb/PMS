namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondings", "TargetProductID", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetAbbr", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetCustomer", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPO", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetWeight", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetDetailRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetDimensionActual", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateLastWeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateOtherRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "PlatePerson", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPreProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPreProcessPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPreProcessCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "PlatePreProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "PlatePreProcessPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "PlatePreProcessCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "WeldCuStringDiameter", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldHold", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "WarpageFix", c => c.String());
            AddColumn("dbo.RecordBondings", "WarpagePerson", c => c.String());
            AddColumn("dbo.RecordBondings", "WarpageCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "DimensionCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "DimensionWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "DimensionPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "DimensionCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "BindingCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BindingPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "BindingCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "SprayCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "SprayPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "SprayCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "CleanCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "CleanPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "CleanCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "ApperanceCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "ApperancePerson", c => c.String());
            AddColumn("dbo.RecordBondings", "ApperanceCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "PackCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "PackPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "PackCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordTests", "Defects", c => c.String());
            DropColumn("dbo.RecordBondings", "TargetLot");
            DropColumn("dbo.RecordBondings", "TargetDimension");
            DropColumn("dbo.RecordBondings", "TargetCheckPerson");
            DropColumn("dbo.RecordBondings", "PlateSerialNumber");
            DropColumn("dbo.RecordBondings", "LastWeldMaterial");
            DropColumn("dbo.RecordBondings", "OtherRecord");
            DropColumn("dbo.RecordBondings", "PlateCheckPerson");
            DropColumn("dbo.RecordBondings", "TargetProcessRecord");
            DropColumn("dbo.RecordBondings", "PlateProcessRecord");
            DropColumn("dbo.RecordBondings", "CuStringDiameter");
            DropColumn("dbo.RecordBondings", "BondWarpageFix");
            DropColumn("dbo.RecordBondings", "BondDimensionCheck");
            DropColumn("dbo.RecordBondings", "BondWarpageCheck");
            DropColumn("dbo.RecordBondings", "BondCheck");
            DropColumn("dbo.RecordBondings", "BondCleanCheck");
            DropColumn("dbo.RecordBondings", "BondAppearanceCheck");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordBondings", "BondAppearanceCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondCleanCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondDimensionCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BondWarpageFix", c => c.String());
            AddColumn("dbo.RecordBondings", "CuStringDiameter", c => c.Double(nullable: false));
            AddColumn("dbo.RecordBondings", "PlateProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateCheckPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "OtherRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "LastWeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateSerialNumber", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetCheckPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetDimension", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetLot", c => c.String());
            DropColumn("dbo.RecordTests", "Defects");
            DropColumn("dbo.RecordBondings", "PackCheckTime");
            DropColumn("dbo.RecordBondings", "PackPerson");
            DropColumn("dbo.RecordBondings", "PackCheck");
            DropColumn("dbo.RecordBondings", "ApperanceCheckTime");
            DropColumn("dbo.RecordBondings", "ApperancePerson");
            DropColumn("dbo.RecordBondings", "ApperanceCheck");
            DropColumn("dbo.RecordBondings", "CleanCheckTime");
            DropColumn("dbo.RecordBondings", "CleanPerson");
            DropColumn("dbo.RecordBondings", "CleanCheck");
            DropColumn("dbo.RecordBondings", "SprayCheckTime");
            DropColumn("dbo.RecordBondings", "SprayPerson");
            DropColumn("dbo.RecordBondings", "SprayCheck");
            DropColumn("dbo.RecordBondings", "BindingCheckTime");
            DropColumn("dbo.RecordBondings", "BindingPerson");
            DropColumn("dbo.RecordBondings", "BindingCheck");
            DropColumn("dbo.RecordBondings", "DimensionCheckTime");
            DropColumn("dbo.RecordBondings", "DimensionPerson");
            DropColumn("dbo.RecordBondings", "DimensionWarpageCheck");
            DropColumn("dbo.RecordBondings", "DimensionCheck");
            DropColumn("dbo.RecordBondings", "WarpageCheckTime");
            DropColumn("dbo.RecordBondings", "WarpagePerson");
            DropColumn("dbo.RecordBondings", "WarpageFix");
            DropColumn("dbo.RecordBondings", "WeldCheckTime");
            DropColumn("dbo.RecordBondings", "WeldPerson");
            DropColumn("dbo.RecordBondings", "WeldHold");
            DropColumn("dbo.RecordBondings", "WeldCuStringDiameter");
            DropColumn("dbo.RecordBondings", "PlatePreProcessCheckTime");
            DropColumn("dbo.RecordBondings", "PlatePreProcessPerson");
            DropColumn("dbo.RecordBondings", "PlatePreProcessRecord");
            DropColumn("dbo.RecordBondings", "TargetPreProcessCheckTime");
            DropColumn("dbo.RecordBondings", "TargetPreProcessPerson");
            DropColumn("dbo.RecordBondings", "TargetPreProcessRecord");
            DropColumn("dbo.RecordBondings", "PlatePerson");
            DropColumn("dbo.RecordBondings", "PlateOtherRecord");
            DropColumn("dbo.RecordBondings", "PlateLastWeldMaterial");
            DropColumn("dbo.RecordBondings", "TargetPerson");
            DropColumn("dbo.RecordBondings", "TargetDimensionActual");
            DropColumn("dbo.RecordBondings", "TargetDetailRecord");
            DropColumn("dbo.RecordBondings", "TargetWeight");
            DropColumn("dbo.RecordBondings", "TargetPO");
            DropColumn("dbo.RecordBondings", "TargetCustomer");
            DropColumn("dbo.RecordBondings", "TargetAbbr");
            DropColumn("dbo.RecordBondings", "TargetProductID");
        }
    }
}
