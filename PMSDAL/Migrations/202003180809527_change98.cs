namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change98 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSOrderHistories", "PartNumber", c => c.String());
            AddColumn("dbo.PMSOrderHistories", "SecondMachineDimension", c => c.String());
            AddColumn("dbo.PMSOrderHistories", "SecondMachineDetails", c => c.String());
            AddColumn("dbo.PMSOrders", "PartNumber", c => c.String());
            AddColumn("dbo.PMSOrders", "SecondMachineDimension", c => c.String());
            AddColumn("dbo.PMSOrders", "SecondMachineDetails", c => c.String());
            AddColumn("dbo.RecordTestHistories", "LastUpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordTests", "LastUpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "LastUpdateTime");
            DropColumn("dbo.RecordTestHistories", "LastUpdateTime");
            DropColumn("dbo.PMSOrders", "SecondMachineDetails");
            DropColumn("dbo.PMSOrders", "SecondMachineDimension");
            DropColumn("dbo.PMSOrders", "PartNumber");
            DropColumn("dbo.PMSOrderHistories", "SecondMachineDetails");
            DropColumn("dbo.PMSOrderHistories", "SecondMachineDimension");
            DropColumn("dbo.PMSOrderHistories", "PartNumber");
        }
    }
}
