namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change136 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryItemTCBs", "BondingPO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryItemTCBs", "BondingPO");
        }
    }
}
