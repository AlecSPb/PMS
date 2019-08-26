namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change81 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialInventoryInHistories", "MaterialSource", c => c.String());
            AddColumn("dbo.MaterialInventoryIns", "MaterialSource", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialInventoryIns", "MaterialSource");
            DropColumn("dbo.MaterialInventoryInHistories", "MaterialSource");
        }
    }
}
