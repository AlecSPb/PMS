namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change58 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondingHistories", "PlanBatchNumber", c => c.Int(nullable: false));
            AddColumn("dbo.RecordBondings", "PlanBatchNumber", c => c.Int(nullable: false));
            AddColumn("dbo.RecordMillingHistories", "PlanBatchNumber", c => c.Int(nullable: false));
            AddColumn("dbo.RecordMillings", "PlanBatchNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "PlanBatchNumber");
            DropColumn("dbo.RecordMillingHistories", "PlanBatchNumber");
            DropColumn("dbo.RecordBondings", "PlanBatchNumber");
            DropColumn("dbo.RecordBondingHistories", "PlanBatchNumber");
        }
    }
}
