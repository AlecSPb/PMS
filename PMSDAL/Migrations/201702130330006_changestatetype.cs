namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestatetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BDCompounds", "State", c => c.String());
            AddColumn("dbo.BDCustomers", "State", c => c.String());
            AddColumn("dbo.BDDeliveryAddresses", "State", c => c.String());
            AddColumn("dbo.RecordMillings", "State", c => c.String());
            AddColumn("dbo.RecordTakeOuts", "State", c => c.String());
            AddColumn("dbo.RecordVHPItems", "State", c => c.String());
            AddColumn("dbo.RecordVHPs", "State", c => c.String());
            AddColumn("dbo.BDVHPProcesses", "State", c => c.String());
            AlterColumn("dbo.UserAccesses", "State", c => c.String());
            AlterColumn("dbo.UserRoles", "State", c => c.String());
            AlterColumn("dbo.PMSMaterialNeeds", "State", c => c.String());
            AlterColumn("dbo.PMSMaterialOrderItems", "State", c => c.String());
            AlterColumn("dbo.PMSMaterialOrders", "State", c => c.String());
            AlterColumn("dbo.PMSMaterialOrders", "Priority", c => c.String());
            AlterColumn("dbo.PMSOrders", "Priority", c => c.String());
            AlterColumn("dbo.PMSOrders", "State", c => c.String());
            AlterColumn("dbo.PMSPlanVHPs", "State", c => c.String());
            AlterColumn("dbo.RecordDeliveries", "State", c => c.String());
            AlterColumn("dbo.RecordProducts", "State", c => c.String());
            AlterColumn("dbo.Users", "State", c => c.String());
            AlterColumn("dbo.BDVHPDevices", "State", c => c.String());
            AlterColumn("dbo.BDVHPMolds", "State", c => c.String());
            DropColumn("dbo.RecordMillings", "CurrentState");
            DropColumn("dbo.RecordTakeOuts", "CurrentState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordTakeOuts", "CurrentState", c => c.Int(nullable: false));
            AddColumn("dbo.RecordMillings", "CurrentState", c => c.Int(nullable: false));
            AlterColumn("dbo.BDVHPMolds", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.BDVHPDevices", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.RecordProducts", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.RecordDeliveries", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSPlanVHPs", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSOrders", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSOrders", "Priority", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSMaterialOrders", "Priority", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSMaterialOrders", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSMaterialOrderItems", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.PMSMaterialNeeds", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.UserRoles", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAccesses", "State", c => c.Int(nullable: false));
            DropColumn("dbo.BDVHPProcesses", "State");
            DropColumn("dbo.RecordVHPs", "State");
            DropColumn("dbo.RecordVHPItems", "State");
            DropColumn("dbo.RecordTakeOuts", "State");
            DropColumn("dbo.RecordMillings", "State");
            DropColumn("dbo.BDDeliveryAddresses", "State");
            DropColumn("dbo.BDCustomers", "State");
            DropColumn("dbo.BDCompounds", "State");
        }
    }
}
