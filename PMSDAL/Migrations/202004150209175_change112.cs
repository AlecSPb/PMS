namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change112 : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Consumables");
        }
    }
}
