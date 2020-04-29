namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change117 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConsumablePurchases",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Category = c.String(),
                        ItemName = c.String(),
                        Specification = c.String(),
                        Details = c.String(),
                        Quantity = c.Double(nullable: false),
                        QuantityUnit = c.String(),
                        Grade = c.String(),
                        Remark = c.String(),
                        Supplier = c.String(),
                        TotalCost = c.Double(nullable: false),
                        ProcessHistory = c.String(),
                        LastUpdateTime = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ConsumableInventories", "Specification", c => c.String());
            AddColumn("dbo.ConsumableInventories", "Details", c => c.String());
            AddColumn("dbo.ConsumableInventories", "Grade", c => c.String());
            AddColumn("dbo.ConsumableInventories", "StorePosition", c => c.String());
            AddColumn("dbo.ConsumableInventories", "PersonInCharge", c => c.String());
            AddColumn("dbo.ConsumableInventories", "History", c => c.String());
            AddColumn("dbo.ConsumableInventories", "LastUpdateTime", c => c.String());
            DropColumn("dbo.ConsumableInventories", "ItemDetails");
            DropColumn("dbo.ConsumableInventories", "UpdatePerson");
            DropTable("dbo.Consumables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Consumables",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.String(),
                        State = c.String(),
                        ItemName = c.String(),
                        ItemDetails = c.String(),
                        Quantity = c.Double(nullable: false),
                        QuantityUnit = c.String(),
                        ForWho = c.String(),
                        SupplierSource = c.String(),
                        Cost = c.Double(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ConsumableInventories", "UpdatePerson", c => c.String());
            AddColumn("dbo.ConsumableInventories", "ItemDetails", c => c.String());
            DropColumn("dbo.ConsumableInventories", "LastUpdateTime");
            DropColumn("dbo.ConsumableInventories", "History");
            DropColumn("dbo.ConsumableInventories", "PersonInCharge");
            DropColumn("dbo.ConsumableInventories", "StorePosition");
            DropColumn("dbo.ConsumableInventories", "Grade");
            DropColumn("dbo.ConsumableInventories", "Details");
            DropColumn("dbo.ConsumableInventories", "Specification");
            DropTable("dbo.ConsumablePurchases");
        }
    }
}
