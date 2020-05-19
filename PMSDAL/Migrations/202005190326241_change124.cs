namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change124 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deliveries", "LastUpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "LastUpdateTime");
        }
    }
}
