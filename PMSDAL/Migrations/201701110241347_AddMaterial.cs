namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaterial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMSMaterialNeeds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Composition = c.String(),
                        Purity = c.String(),
                        Weight = c.Double(nullable: false),
                        Supplier = c.String(),
                        SpecialNeeds = c.String(),
                        PMIWorkingNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PMSMaterialOrderItems",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.Int(nullable: false),
                        PMIWorkNumber = c.String(),
                        Composition = c.String(),
                        Purity = c.String(),
                        Description = c.String(),
                        ProvideRawMaterial = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        MaterialOrderID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PMSMaterialOrders", t => t.MaterialOrderID)
                .Index(t => t.MaterialOrderID);
            
            CreateTable(
                "dbo.PMSMaterialOrders",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.Int(nullable: false),
                        OrderPO = c.String(),
                        Supplier = c.String(),
                        SupplierAbbr = c.String(),
                        SupplierReceiver = c.String(),
                        SupplierEmail = c.String(),
                        SupplierAddress = c.String(),
                        Remark = c.String(),
                        ShipFee = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PMSMaterialOrderItems", "MaterialOrderID", "dbo.PMSMaterialOrders");
            DropIndex("dbo.PMSMaterialOrderItems", new[] { "MaterialOrderID" });
            DropTable("dbo.PMSMaterialOrders");
            DropTable("dbo.PMSMaterialOrderItems");
            DropTable("dbo.PMSMaterialNeeds");
        }
    }
}
