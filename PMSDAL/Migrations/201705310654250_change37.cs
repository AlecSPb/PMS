namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change37 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialOrderItemHistories", "Priority", c => c.String());
            AddColumn("dbo.MaterialOrderItems", "Priority", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialOrderItems", "Priority");
            DropColumn("dbo.MaterialOrderItemHistories", "Priority");
        }
    }
}
