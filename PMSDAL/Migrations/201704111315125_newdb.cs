namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeliveryItems", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordDeliveryItems", "Creator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordDeliveryItems", "Creator");
            DropColumn("dbo.RecordDeliveryItems", "CreateTime");
        }
    }
}
