namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change76 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryHistories", "DeliveryExpress", c => c.String());
            AddColumn("dbo.Deliveries", "DeliveryExpress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "DeliveryExpress");
            DropColumn("dbo.DeliveryHistories", "DeliveryExpress");
        }
    }
}
