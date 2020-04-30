namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change118 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "SampleNeedRemark", c => c.String());
            AddColumn("dbo.PMSOrderHistories", "SampleForAnlysisRemark", c => c.String());
            AddColumn("dbo.PMSOrders", "SampleNeedRemark", c => c.String());
            AddColumn("dbo.PMSOrders", "SampleForAnlysisRemark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "SampleForAnlysisRemark");
            DropColumn("dbo.PMSOrders", "SampleNeedRemark");
            DropColumn("dbo.PMSOrderHistories", "SampleForAnlysisRemark");
            DropColumn("dbo.PMSOrderHistories", "SampleNeedRemark");
        }
    }
}
