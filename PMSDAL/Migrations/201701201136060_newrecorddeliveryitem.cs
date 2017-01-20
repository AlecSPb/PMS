namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newrecorddeliveryitem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecordDeliveryItems", "ProductRecordID", "dbo.RecordProducts");
            DropIndex("dbo.RecordDeliveryItems", new[] { "ProductRecordID" });
            AddColumn("dbo.RecordDeliveryItems", "ProductID", c => c.String());
            AddColumn("dbo.RecordDeliveryItems", "Composition", c => c.String());
            AddColumn("dbo.RecordDeliveryItems", "Customer", c => c.String());
            AddColumn("dbo.RecordDeliveryItems", "PO", c => c.String());
            AddColumn("dbo.RecordDeliveryItems", "Weight", c => c.String());
            DropColumn("dbo.RecordDeliveryItems", "ProductRecordID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordDeliveryItems", "ProductRecordID", c => c.Guid());
            DropColumn("dbo.RecordDeliveryItems", "Weight");
            DropColumn("dbo.RecordDeliveryItems", "PO");
            DropColumn("dbo.RecordDeliveryItems", "Customer");
            DropColumn("dbo.RecordDeliveryItems", "Composition");
            DropColumn("dbo.RecordDeliveryItems", "ProductID");
            CreateIndex("dbo.RecordDeliveryItems", "ProductRecordID");
            AddForeignKey("dbo.RecordDeliveryItems", "ProductRecordID", "dbo.RecordProducts", "ID");
        }
    }
}
