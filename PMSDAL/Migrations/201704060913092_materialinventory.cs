namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class materialinventory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMSMaterialInventoryIns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        State = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Composition = c.String(),
                        Purity = c.String(),
                        Receiver = c.String(),
                        Weight = c.Double(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PMSMaterialInventoryOuts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        State = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Composition = c.String(),
                        Purity = c.String(),
                        Supplier = c.String(),
                        Weight = c.Double(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.PMSMaterialNeeds", "Supplier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PMSMaterialNeeds", "Supplier", c => c.String());
            DropTable("dbo.PMSMaterialInventoryOuts");
            DropTable("dbo.PMSMaterialInventoryIns");
        }
    }
}
