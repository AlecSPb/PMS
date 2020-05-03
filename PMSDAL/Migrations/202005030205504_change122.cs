namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change122 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConsumableInventories", "LastUpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConsumableInventories", "LastUpdateTime", c => c.String());
        }
    }
}
