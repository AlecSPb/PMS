namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeMoldHistories", "PlanType", c => c.String());
            AddColumn("dbo.RecordDeMolds", "PlanType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordDeMolds", "PlanType");
            DropColumn("dbo.RecordDeMoldHistories", "PlanType");
        }
    }
}
