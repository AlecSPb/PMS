namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change116 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryItemHistories", "DeliveryType", c => c.String());
            AddColumn("dbo.DeliveryItems", "DeliveryType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryItems", "DeliveryType");
            DropColumn("dbo.DeliveryItemHistories", "DeliveryType");
        }
    }
}
