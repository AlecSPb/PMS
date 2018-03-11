namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change56 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "Progress", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "Progress");
        }
    }
}
