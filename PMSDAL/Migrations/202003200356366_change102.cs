namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change102 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drawings", "DrawID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drawings", "DrawID");
        }
    }
}
