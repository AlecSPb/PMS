namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcompostion2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeMolds", "Temperature1", c => c.String());
            AddColumn("dbo.RecordDeMolds", "Temperature2", c => c.String());
            AddColumn("dbo.RecordDeMolds", "Remark", c => c.String());
            AddColumn("dbo.RecordDeMolds", "Weight", c => c.Double(nullable: false));
            DropColumn("dbo.RecordDeMolds", "MoveOutTemperature");
            DropColumn("dbo.RecordDeMolds", "TakeOutTemperature");
            DropColumn("dbo.RecordDeMolds", "ExtraInformation");
            DropColumn("dbo.RecordDeMolds", "RoughTargetWeight");
            DropColumn("dbo.RecordDeMolds", "WithExtraThickness");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordDeMolds", "WithExtraThickness", c => c.String());
            AddColumn("dbo.RecordDeMolds", "RoughTargetWeight", c => c.Double(nullable: false));
            AddColumn("dbo.RecordDeMolds", "ExtraInformation", c => c.String());
            AddColumn("dbo.RecordDeMolds", "TakeOutTemperature", c => c.String());
            AddColumn("dbo.RecordDeMolds", "MoveOutTemperature", c => c.String());
            DropColumn("dbo.RecordDeMolds", "Weight");
            DropColumn("dbo.RecordDeMolds", "Remark");
            DropColumn("dbo.RecordDeMolds", "Temperature2");
            DropColumn("dbo.RecordDeMolds", "Temperature1");
        }
    }
}
