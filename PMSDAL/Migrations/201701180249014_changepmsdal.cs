namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepmsdal : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PMSProducts", newName: "RecordProducts");
            AddColumn("dbo.RecordProducts", "Sample", c => c.String());
            DropTable("dbo.PMSSamples");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PMSSamples",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductID = c.String(),
                        Composition = c.String(),
                        CompositionAbbr = c.String(),
                        PO = c.String(),
                        Customer = c.String(),
                        Weight1 = c.String(),
                        Weight2 = c.String(),
                        Weight3 = c.String(),
                        Weight4 = c.String(),
                        ForTarget = c.String(),
                        Remark = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.RecordProducts", "Sample");
            RenameTable(name: "dbo.RecordProducts", newName: "PMSProducts");
        }
    }
}
