namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change55 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "Priority", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "Priority", c => c.Int(nullable: false));
        }
    }
}
