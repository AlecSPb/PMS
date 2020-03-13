namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change93 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Samples", "OriginalRequirement", c => c.String());
            DropColumn("dbo.Samples", "OriginalRequirment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Samples", "OriginalRequirment", c => c.String());
            DropColumn("dbo.Samples", "OriginalRequirement");
        }
    }
}
