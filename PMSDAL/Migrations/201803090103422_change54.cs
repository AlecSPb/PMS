namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change54 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "PersonInCharge", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "PersonInCharge", c => c.DateTime(nullable: false));
        }
    }
}
