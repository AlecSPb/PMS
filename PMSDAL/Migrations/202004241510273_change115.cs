namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change115 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumableInventories", "Category", c => c.String());
            AddColumn("dbo.ConsumableInventories", "UpdatePerson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsumableInventories", "UpdatePerson");
            DropColumn("dbo.ConsumableInventories", "Category");
        }
    }
}
