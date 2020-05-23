namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change127 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToolSieves", "BoxNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToolSieves", "BoxNumber");
        }
    }
}
