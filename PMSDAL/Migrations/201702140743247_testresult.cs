namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testresult : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RecordProducts", newName: "RecordTestResults");
            AddColumn("dbo.RecordTestResults", "TestType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTestResults", "TestType");
            RenameTable(name: "dbo.RecordTestResults", newName: "RecordProducts");
        }
    }
}
