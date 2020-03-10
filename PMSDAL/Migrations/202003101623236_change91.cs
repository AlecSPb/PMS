namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change91 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryChases",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductID = c.String(),
                        Composition = c.String(),
                        PO = c.String(),
                        PMINumber = c.String(),
                        Customer = c.String(),
                        Dimension = c.String(),
                        Weight = c.Double(nullable: false),
                        TraceInformation = c.String(),
                        Remark = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RawMaterialSheets", "Composition", c => c.String());
            AddColumn("dbo.Samples", "TraceInformation", c => c.String());
            DropColumn("dbo.Samples", "Process");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Samples", "Process", c => c.String());
            DropColumn("dbo.Samples", "TraceInformation");
            DropColumn("dbo.RawMaterialSheets", "Composition");
            DropTable("dbo.DeliveryChases");
        }
    }
}
