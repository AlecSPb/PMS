namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OutSourceHistories", "OrderLot", c => c.String());
            AddColumn("dbo.OutSourceHistories", "OrderType", c => c.String());
            AddColumn("dbo.OutSources", "OrderLot", c => c.String());
            AddColumn("dbo.OutSources", "OrderType", c => c.String());
            DropColumn("dbo.FeedBacks", "ReceivedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedBacks", "ReceivedDate", c => c.String());
            DropColumn("dbo.OutSources", "OrderType");
            DropColumn("dbo.OutSources", "OrderLot");
            DropColumn("dbo.OutSourceHistories", "OrderType");
            DropColumn("dbo.OutSourceHistories", "OrderLot");
        }
    }
}
