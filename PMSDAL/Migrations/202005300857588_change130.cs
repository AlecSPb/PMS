namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change130 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "OrderRemark", c => c.String());
            AddColumn("dbo.PMSOrders", "OrderRemark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "OrderRemark");
            DropColumn("dbo.PMSOrderHistories", "OrderRemark");
        }
    }
}
