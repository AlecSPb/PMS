namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change96 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialInventoryInHistories", "SupplierPO", c => c.String());
            AddColumn("dbo.MaterialInventoryIns", "SupplierPO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialInventoryIns", "SupplierPO");
            DropColumn("dbo.MaterialInventoryInHistories", "SupplierPO");
        }
    }
}
