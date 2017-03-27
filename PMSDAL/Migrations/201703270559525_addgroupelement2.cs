namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgroupelement2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BDElementGroupItems", "OrderNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BDElementGroupItems", "OrderNumber");
        }
    }
}
