namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckListHistories", "Introduction", c => c.String());
            AddColumn("dbo.CheckLists", "Introduction", c => c.String());
            AddColumn("dbo.MaterialOrderHistories", "FinishTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MaterialOrders", "FinishTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSOrderHistories", "FinishTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSOrders", "FinishTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.OutSourceHistories", "FinishTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.OutSourceHistories", "PaidState", c => c.String());
            AddColumn("dbo.OutSources", "FinishTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.OutSources", "PaidState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OutSources", "PaidState");
            DropColumn("dbo.OutSources", "FinishTime");
            DropColumn("dbo.OutSourceHistories", "PaidState");
            DropColumn("dbo.OutSourceHistories", "FinishTime");
            DropColumn("dbo.PMSOrders", "FinishTime");
            DropColumn("dbo.PMSOrderHistories", "FinishTime");
            DropColumn("dbo.MaterialOrders", "FinishTime");
            DropColumn("dbo.MaterialOrderHistories", "FinishTime");
            DropColumn("dbo.CheckLists", "Introduction");
            DropColumn("dbo.CheckListHistories", "Introduction");
        }
    }
}
