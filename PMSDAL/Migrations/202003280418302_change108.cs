namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change108 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryHistories", "PackageWeight", c => c.String());
            AddColumn("dbo.Deliveries", "PackageWeight", c => c.String());
        }
        
        public override void Down()
        {

        }
    }
}
