namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change123 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConsumablePurchases", "LastUpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConsumablePurchases", "LastUpdateTime", c => c.String());
        }
    }
}
