namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change57 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "Type");
        }
    }
}
