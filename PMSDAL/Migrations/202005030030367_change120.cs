namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change120 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumableInventories", "MaxWarningQuantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsumableInventories", "MaxWarningQuantity");
        }
    }
}
