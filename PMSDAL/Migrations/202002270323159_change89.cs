namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change89 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMICounters", "RowOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMICounters", "RowOrder");
        }
    }
}
