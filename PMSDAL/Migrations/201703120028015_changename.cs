namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RecordTestResults", newName: "RecordTests");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RecordTests", newName: "RecordTestResults");
        }
    }
}
