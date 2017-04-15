namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrders", "ReviewTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSOrders", "Reviewer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSOrders", "Reviewer");
            DropColumn("dbo.PMSOrders", "ReviewTime");
        }
    }
}
