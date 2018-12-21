namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change67 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordTestHistories", "Roughness", c => c.String());
            AddColumn("dbo.RecordTests", "Roughness", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "Roughness");
            DropColumn("dbo.RecordTestHistories", "Roughness");
        }
    }
}
