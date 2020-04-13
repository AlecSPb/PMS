namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialInventoryInHistories", "GDMS", c => c.String());
            AddColumn("dbo.MaterialInventoryInHistories", "ICPOES", c => c.String());
            AddColumn("dbo.MaterialInventoryIns", "GDMS", c => c.String());
            AddColumn("dbo.MaterialInventoryIns", "ICPOES", c => c.String());
            AddColumn("dbo.RawMaterialSheets", "IsSampleTaking", c => c.Boolean(nullable: false));
            AddColumn("dbo.RawMaterialSheets", "SampleTakingTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RawMaterialSheets", "GDMS", c => c.String());
            AddColumn("dbo.RawMaterialSheets", "ICPOES", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RawMaterialSheets", "ICPOES");
            DropColumn("dbo.RawMaterialSheets", "GDMS");
            DropColumn("dbo.RawMaterialSheets", "SampleTakingTime");
            DropColumn("dbo.RawMaterialSheets", "IsSampleTaking");
            DropColumn("dbo.MaterialInventoryIns", "ICPOES");
            DropColumn("dbo.MaterialInventoryIns", "GDMS");
            DropColumn("dbo.MaterialInventoryInHistories", "ICPOES");
            DropColumn("dbo.MaterialInventoryInHistories", "GDMS");
        }
    }
}
