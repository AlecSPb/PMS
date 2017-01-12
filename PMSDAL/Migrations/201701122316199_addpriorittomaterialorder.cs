namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpriorittomaterialorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSMaterialOrders", "Priorty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSMaterialOrders", "Priorty");
        }
    }
}
