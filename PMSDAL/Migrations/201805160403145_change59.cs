namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change59 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialInventoryInHistories", "QuickRemark", c => c.String());
            AddColumn("dbo.MaterialInventoryIns", "QuickRemark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialInventoryIns", "QuickRemark");
            DropColumn("dbo.MaterialInventoryInHistories", "QuickRemark");
        }
    }
}
