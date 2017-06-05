namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change38 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        ItemType = c.String(),
                        ItemName = c.String(),
                        Specification = c.String(),
                        Quantity = c.Double(nullable: false),
                        Unit = c.String(),
                        NeedPerson = c.String(),
                        PurchasePerson = c.String(),
                        Cost = c.Double(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Purchases");
        }
    }
}
