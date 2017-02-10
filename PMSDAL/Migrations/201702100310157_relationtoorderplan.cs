namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationtoorderplan : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PMSPlanVHPs", "OrderID");
            AddForeignKey("dbo.PMSPlanVHPs", "OrderID", "dbo.PMSOrders", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PMSPlanVHPs", "OrderID", "dbo.PMSOrders");
            DropIndex("dbo.PMSPlanVHPs", new[] { "OrderID" });
        }
    }
}
