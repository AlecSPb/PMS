namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editmi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSMaterialInventoryIns", "MaterialLot", c => c.String());
            AddColumn("dbo.PMSMaterialInventoryIns", "Supplier", c => c.String());
            AddColumn("dbo.PMSMaterialInventoryOuts", "MaterialLot", c => c.String());
            AddColumn("dbo.PMSMaterialInventoryOuts", "Receiver", c => c.String());
            DropColumn("dbo.PMSMaterialInventoryIns", "Receiver");
            DropColumn("dbo.PMSMaterialInventoryOuts", "Supplier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PMSMaterialInventoryOuts", "Supplier", c => c.String());
            AddColumn("dbo.PMSMaterialInventoryIns", "Receiver", c => c.String());
            DropColumn("dbo.PMSMaterialInventoryOuts", "Receiver");
            DropColumn("dbo.PMSMaterialInventoryOuts", "MaterialLot");
            DropColumn("dbo.PMSMaterialInventoryIns", "Supplier");
            DropColumn("dbo.PMSMaterialInventoryIns", "MaterialLot");
        }
    }
}
