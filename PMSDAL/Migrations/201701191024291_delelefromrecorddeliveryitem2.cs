namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delelefromrecorddeliveryitem2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeliveryItems", "ProductRecordID", c => c.String());
            DropColumn("dbo.RecordDeliveryItems", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordDeliveryItems", "ProductID", c => c.String());
            DropColumn("dbo.RecordDeliveryItems", "ProductRecordID");
        }
    }
}
