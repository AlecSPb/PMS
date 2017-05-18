namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryHistories", "FinishTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Deliveries", "FinishTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "FinishTime");
            DropColumn("dbo.DeliveryHistories", "FinishTime");
        }
    }
}
