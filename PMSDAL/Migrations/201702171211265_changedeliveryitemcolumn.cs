namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedeliveryitemcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeliveryItems", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordDeliveryItems", "State");
        }
    }
}
