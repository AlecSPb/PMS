namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change78 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordMillingHistories", "RecycleID", c => c.String());
            AddColumn("dbo.RecordMillings", "RecycleID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "RecycleID");
            DropColumn("dbo.RecordMillingHistories", "RecycleID");
        }
    }
}
