namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change87 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordMillingHistories", "SieveDescription", c => c.String());
            AddColumn("dbo.RecordMillings", "SieveDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "SieveDescription");
            DropColumn("dbo.RecordMillingHistories", "SieveDescription");
        }
    }
}
