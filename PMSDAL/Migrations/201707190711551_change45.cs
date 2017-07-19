namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change45 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordTestHistories", "FollowUps", c => c.String());
            AddColumn("dbo.RecordTests", "FollowUps", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "FollowUps");
            DropColumn("dbo.RecordTestHistories", "FollowUps");
        }
    }
}
