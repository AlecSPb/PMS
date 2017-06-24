namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialInventoryOutHistories", "ActualWeight", c => c.Double(nullable: false));
            AddColumn("dbo.MaterialInventoryOuts", "ActualWeight", c => c.Double(nullable: false));
            AddColumn("dbo.RecordBondings", "PlateType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordBondings", "PlateType");
            DropColumn("dbo.MaterialInventoryOuts", "ActualWeight");
            DropColumn("dbo.MaterialInventoryOutHistories", "ActualWeight");
        }
    }
}
