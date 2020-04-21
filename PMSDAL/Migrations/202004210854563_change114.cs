namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change114 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConsumableInventories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ItemName = c.String(),
                        ItemDetails = c.String(),
                        Quantity = c.Double(nullable: false),
                        QuantityUnit = c.String(),
                        MinWarningQuantity = c.Double(nullable: false),
                        Remark = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SimpleMaterials",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ElementName = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        Remark = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SimpleMaterials");
            DropTable("dbo.ConsumableInventories");
        }
    }
}
