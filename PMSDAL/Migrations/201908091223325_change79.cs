namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change79 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMICounters", "ItemHistory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMICounters", "ItemHistory");
        }
    }
}
