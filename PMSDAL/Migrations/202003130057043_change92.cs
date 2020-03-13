namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change92 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Samples", "OriginalRequirment", c => c.String());
            DropColumn("dbo.Samples", "OrginalRequirment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Samples", "OrginalRequirment", c => c.String());
            DropColumn("dbo.Samples", "OriginalRequirment");
        }
    }
}
