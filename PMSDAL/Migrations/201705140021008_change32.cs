namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OutSourceHistories", "Dimension", c => c.String());
            AddColumn("dbo.OutSources", "Dimension", c => c.String());
            DropColumn("dbo.FeedBacks", "FeedbackReason");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedBacks", "FeedbackReason", c => c.String());
            DropColumn("dbo.OutSources", "Dimension");
            DropColumn("dbo.OutSourceHistories", "Dimension");
        }
    }
}
