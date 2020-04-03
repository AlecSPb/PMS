namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change109 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMICounters", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMICounters", "Remark");
        }
    }
}
