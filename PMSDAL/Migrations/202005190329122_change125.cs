namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change125 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryHistories", "LastUpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryHistories", "LastUpdateTime");
        }
    }
}
