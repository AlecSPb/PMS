namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change71 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordTestHistories", "QC", c => c.String());
            AddColumn("dbo.RecordTests", "QC", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "QC");
            DropColumn("dbo.RecordTestHistories", "QC");
        }
    }
}
