namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change121 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumableInventories", "CountHistory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsumableInventories", "CountHistory");
        }
    }
}
