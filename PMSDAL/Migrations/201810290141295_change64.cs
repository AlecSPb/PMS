namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change64 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Failures", "Composition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Failures", "Composition");
        }
    }
}
