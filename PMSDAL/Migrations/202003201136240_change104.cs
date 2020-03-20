namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change104 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "LastUpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSOrders", "LastUpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "LastUpdateTime");
            DropColumn("dbo.PMSOrderHistories", "LastUpdateTime");
        }
    }
}
