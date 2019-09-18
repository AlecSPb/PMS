namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change83 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialOrderItemHistories", "Remark", c => c.String());
            AddColumn("dbo.MaterialOrderItems", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialOrderItems", "Remark");
            DropColumn("dbo.MaterialOrderItemHistories", "Remark");
        }
    }
}
