namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change95 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Samples", "SampleID", c => c.String());
            AddColumn("dbo.Samples", "ICPOES", c => c.String());
            AddColumn("dbo.Samples", "GDMS", c => c.String());
            AddColumn("dbo.Samples", "IGA", c => c.String());
            AddColumn("dbo.Samples", "Thermal", c => c.String());
            AddColumn("dbo.Samples", "Permittivity", c => c.String());
            AddColumn("dbo.Samples", "OtherTestResult", c => c.String());
            AddColumn("dbo.Samples", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Samples", "SampleType", c => c.String());
            DropColumn("dbo.Samples", "TestResult");
            DropColumn("dbo.Samples", "MoreTestResult");
            DropColumn("dbo.Samples", "SampleShape");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Samples", "SampleShape", c => c.String());
            AddColumn("dbo.Samples", "MoreTestResult", c => c.String());
            AddColumn("dbo.Samples", "TestResult", c => c.String());
            DropColumn("dbo.Samples", "SampleType");
            DropColumn("dbo.Samples", "Quantity");
            DropColumn("dbo.Samples", "OtherTestResult");
            DropColumn("dbo.Samples", "Permittivity");
            DropColumn("dbo.Samples", "Thermal");
            DropColumn("dbo.Samples", "IGA");
            DropColumn("dbo.Samples", "GDMS");
            DropColumn("dbo.Samples", "ICPOES");
            DropColumn("dbo.Samples", "SampleID");
        }
    }
}
