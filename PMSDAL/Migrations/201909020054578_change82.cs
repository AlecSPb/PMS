namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change82 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordMillingHistories", "Details", c => c.String());
            AddColumn("dbo.RecordMillings", "Details", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "Details");
            DropColumn("dbo.RecordMillingHistories", "Details");
        }
    }
}
