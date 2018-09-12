namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change62 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialOrderItemHistories", "MaterialPrice", c => c.Double(nullable: false));
            AddColumn("dbo.MaterialOrderItems", "MaterialPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialOrderItems", "MaterialPrice");
            DropColumn("dbo.MaterialOrderItemHistories", "MaterialPrice");
        }
    }
}
