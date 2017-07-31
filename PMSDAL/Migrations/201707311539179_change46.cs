namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change46 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordMillingHistories", "GrainSize", c => c.String());
            AddColumn("dbo.RecordMillings", "GrainSize", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "GrainSize");
            DropColumn("dbo.RecordMillingHistories", "GrainSize");
        }
    }
}
