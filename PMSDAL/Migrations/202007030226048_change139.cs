namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change139 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "LaserNeed", c => c.String());
            AddColumn("dbo.PMSOrders", "LaserNeed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "LaserNeed");
            DropColumn("dbo.PMSOrderHistories", "LaserNeed");
        }
    }
}
