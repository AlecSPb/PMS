namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abbr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeliveryItems", "Abbr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordDeliveryItems", "Abbr");
        }
    }
}
