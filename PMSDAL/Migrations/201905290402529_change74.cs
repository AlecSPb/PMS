namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change74 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordTestHistories", "BackingPlateLot", c => c.String());
            AddColumn("dbo.RecordTests", "BackingPlateLot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "BackingPlateLot");
            DropColumn("dbo.RecordTestHistories", "BackingPlateLot");
        }
    }
}
