namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class millingchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordMillings", "Composition", c => c.String());
            AddColumn("dbo.RecordMillings", "MaterialSource", c => c.String());
            AddColumn("dbo.RecordMillings", "Remark", c => c.String());
            AddColumn("dbo.RecordMillings", "WeightIn", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "WeightOut", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "WeightRemain", c => c.Double(nullable: false));
            AlterColumn("dbo.RecordMillings", "CreateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.RecordMillings", "RawMaterial");
            DropColumn("dbo.RecordMillings", "FromWho");
            DropColumn("dbo.RecordMillings", "ExtraInformation");
            DropColumn("dbo.RecordMillings", "MaterialIn");
            DropColumn("dbo.RecordMillings", "MaterialOut");
            DropColumn("dbo.RecordMillings", "MaterialRemain");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordMillings", "MaterialRemain", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "MaterialOut", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "MaterialIn", c => c.Double(nullable: false));
            AddColumn("dbo.RecordMillings", "ExtraInformation", c => c.String());
            AddColumn("dbo.RecordMillings", "FromWho", c => c.String());
            AddColumn("dbo.RecordMillings", "RawMaterial", c => c.String());
            AlterColumn("dbo.RecordMillings", "CreateTime", c => c.String());
            DropColumn("dbo.RecordMillings", "WeightRemain");
            DropColumn("dbo.RecordMillings", "WeightOut");
            DropColumn("dbo.RecordMillings", "WeightIn");
            DropColumn("dbo.RecordMillings", "Remark");
            DropColumn("dbo.RecordMillings", "MaterialSource");
            DropColumn("dbo.RecordMillings", "Composition");
        }
    }
}
