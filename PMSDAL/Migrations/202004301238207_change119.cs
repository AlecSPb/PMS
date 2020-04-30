namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change119 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Samples", "OriginalRequirementRemark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Samples", "OriginalRequirementRemark");
        }
    }
}
