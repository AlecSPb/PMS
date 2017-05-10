namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedBacks", "FeedbackReason", c => c.String());
            AddColumn("dbo.ItemDebitHistories", "Unit", c => c.Double(nullable: false));
            AddColumn("dbo.ItemDebits", "Unit", c => c.Double(nullable: false));
            DropColumn("dbo.FeedBacks", "FeebackReason");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedBacks", "FeebackReason", c => c.String());
            DropColumn("dbo.ItemDebits", "Unit");
            DropColumn("dbo.ItemDebitHistories", "Unit");
            DropColumn("dbo.FeedBacks", "FeedbackReason");
        }
    }
}
