namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delelefromrecorddeliveryitem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RecordDeliveryItems", "Composition");
            DropColumn("dbo.RecordDeliveryItems", "Customer");
            DropColumn("dbo.RecordDeliveryItems", "PO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordDeliveryItems", "PO", c => c.String());
            AddColumn("dbo.RecordDeliveryItems", "Customer", c => c.String());
            AddColumn("dbo.RecordDeliveryItems", "Composition", c => c.String());
        }
    }
}
