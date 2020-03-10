namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change90 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasicDictionaries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        Description = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RawMaterialSheets",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Lot = c.String(),
                        Supplier = c.String(),
                        Weight = c.Double(nullable: false),
                        Remark = c.String(),
                        StoreTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Samples",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SampleType = c.String(),
                        ProductID = c.String(),
                        Composition = c.String(),
                        PMINumber = c.String(),
                        MoreInformation = c.String(),
                        Customer = c.String(),
                        OrginalRequirment = c.String(),
                        Process = c.String(),
                        TestResult = c.String(),
                        MoreTestResult = c.String(),
                        Remark = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RecordMachineHistories", "HasPreparedSample", c => c.String());
            AddColumn("dbo.RecordMachines", "HasPreparedSample", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMachines", "HasPreparedSample");
            DropColumn("dbo.RecordMachineHistories", "HasPreparedSample");
            DropTable("dbo.Samples");
            DropTable("dbo.RawMaterialSheets");
            DropTable("dbo.BasicDictionaries");
        }
    }
}
