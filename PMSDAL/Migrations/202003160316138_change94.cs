namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change94 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Samples", "TrackingStage", c => c.String());
            AddColumn("dbo.Samples", "PO", c => c.String());
            AddColumn("dbo.Samples", "Weight", c => c.String());
            AddColumn("dbo.Samples", "SampleShape", c => c.String());
            AddColumn("dbo.Samples", "SampleFor", c => c.String());
            DropColumn("dbo.Samples", "SampleType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Samples", "SampleType", c => c.String());
            DropColumn("dbo.Samples", "SampleFor");
            DropColumn("dbo.Samples", "SampleShape");
            DropColumn("dbo.Samples", "Weight");
            DropColumn("dbo.Samples", "PO");
            DropColumn("dbo.Samples", "TrackingStage");
        }
    }
}
