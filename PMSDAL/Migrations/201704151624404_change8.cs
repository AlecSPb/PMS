namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PMSOrders", "ReviewPassed");
            DropColumn("dbo.PMSOrders", "Reviewer");
            DropColumn("dbo.PMSOrders", "ReviewDate");
            DropColumn("dbo.PMSOrders", "PolicyContent");
            DropColumn("dbo.PMSOrders", "PolicyMaker");
            DropColumn("dbo.PMSOrders", "PolicyMakeDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PMSOrders", "PolicyMakeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSOrders", "PolicyMaker", c => c.String());
            AddColumn("dbo.PMSOrders", "PolicyContent", c => c.String());
            AddColumn("dbo.PMSOrders", "ReviewDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSOrders", "Reviewer", c => c.String());
            AddColumn("dbo.PMSOrders", "ReviewPassed", c => c.Boolean(nullable: false));
        }
    }
}
