namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change142 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordTestHistories", "LaserEngraved", c => c.String());
            AddColumn("dbo.RecordTests", "LaserEngraved", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "LaserEngraved");
            DropColumn("dbo.RecordTestHistories", "LaserEngraved");
        }
    }
}
