namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change106 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryHistories", "LastCheckIDCollection", c => c.String());
            AddColumn("dbo.DeliveryHistories", "IsCustomerSigned", c => c.Boolean(nullable: false));
            AddColumn("dbo.Deliveries", "IsCustomerSigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "IsCustomerSigned");
            DropColumn("dbo.DeliveryHistories", "IsCustomerSigned");
            DropColumn("dbo.DeliveryHistories", "LastCheckIDCollection");
        }
    }
}
