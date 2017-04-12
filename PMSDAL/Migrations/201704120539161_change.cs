namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSMaterialOrderItems", "OrderItemNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSMaterialOrderItems", "OrderItemNumber");
        }
    }
}
