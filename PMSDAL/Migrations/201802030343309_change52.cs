namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change52 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordMillingHistories", "MaterialType", c => c.String());
            AddColumn("dbo.RecordMillings", "MaterialType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "MaterialType");
            DropColumn("dbo.RecordMillingHistories", "MaterialType");
        }
    }
}
