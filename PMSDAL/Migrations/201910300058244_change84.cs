namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change84 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordTestHistories", "CScan", c => c.String());
            AddColumn("dbo.RecordTests", "CScan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "CScan");
            DropColumn("dbo.RecordTestHistories", "CScan");
        }
    }
}
