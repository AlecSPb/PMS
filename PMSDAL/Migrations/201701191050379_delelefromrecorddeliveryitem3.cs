namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delelefromrecorddeliveryitem3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecordDeliveryItems", "ProductRecordID", c => c.Guid());
            CreateIndex("dbo.RecordDeliveryItems", "ProductRecordID");
            AddForeignKey("dbo.RecordDeliveryItems", "ProductRecordID", "dbo.RecordProducts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecordDeliveryItems", "ProductRecordID", "dbo.RecordProducts");
            DropIndex("dbo.RecordDeliveryItems", new[] { "ProductRecordID" });
            AlterColumn("dbo.RecordDeliveryItems", "ProductRecordID", c => c.String());
        }
    }
}
