namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetablenames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PMSAccesses", newName: "UserAccesses");
            RenameTable(name: "dbo.PMSRoles", newName: "UserRoles");
            RenameTable(name: "dbo.Compounds", newName: "BDCompounds");
            RenameTable(name: "dbo.Customers", newName: "BDCustomers");
            RenameTable(name: "dbo.DeliveryAddresses", newName: "BDDeliveryAddresses");
            RenameTable(name: "dbo.PMSUsers", newName: "Users");
            RenameTable(name: "dbo.VHPDevices", newName: "BDVHPDevices");
            RenameTable(name: "dbo.VHPMolds", newName: "BDVHPMolds");
            RenameTable(name: "dbo.VHPProcesses", newName: "BDVHPProcesses");
            DropForeignKey("dbo.PMSRolePMSAccesses", "PMSRole_ID", "dbo.PMSRoles");
            DropForeignKey("dbo.PMSRolePMSAccesses", "PMSAccess_ID", "dbo.PMSAccesses");
            DropIndex("dbo.PMSRolePMSAccesses", new[] { "PMSRole_ID" });
            DropIndex("dbo.PMSRolePMSAccesses", new[] { "PMSAccess_ID" });
            CreateTable(
                "dbo.UserRoleUserAccesses",
                c => new
                    {
                        UserRole_ID = c.Guid(nullable: false),
                        UserAccess_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_ID, t.UserAccess_ID })
                .ForeignKey("dbo.UserRoles", t => t.UserRole_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserAccesses", t => t.UserAccess_ID, cascadeDelete: true)
                .Index(t => t.UserRole_ID)
                .Index(t => t.UserAccess_ID);
            
            DropTable("dbo.PMSRolePMSAccesses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PMSRolePMSAccesses",
                c => new
                    {
                        PMSRole_ID = c.Guid(nullable: false),
                        PMSAccess_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PMSRole_ID, t.PMSAccess_ID });
            
            DropForeignKey("dbo.UserRoleUserAccesses", "UserAccess_ID", "dbo.UserAccesses");
            DropForeignKey("dbo.UserRoleUserAccesses", "UserRole_ID", "dbo.UserRoles");
            DropIndex("dbo.UserRoleUserAccesses", new[] { "UserAccess_ID" });
            DropIndex("dbo.UserRoleUserAccesses", new[] { "UserRole_ID" });
            DropTable("dbo.UserRoleUserAccesses");
            CreateIndex("dbo.PMSRolePMSAccesses", "PMSAccess_ID");
            CreateIndex("dbo.PMSRolePMSAccesses", "PMSRole_ID");
            AddForeignKey("dbo.PMSRolePMSAccesses", "PMSAccess_ID", "dbo.PMSAccesses", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PMSRolePMSAccesses", "PMSRole_ID", "dbo.PMSRoles", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.BDVHPProcesses", newName: "VHPProcesses");
            RenameTable(name: "dbo.BDVHPMolds", newName: "VHPMolds");
            RenameTable(name: "dbo.BDVHPDevices", newName: "VHPDevices");
            RenameTable(name: "dbo.Users", newName: "PMSUsers");
            RenameTable(name: "dbo.BDDeliveryAddresses", newName: "DeliveryAddresses");
            RenameTable(name: "dbo.BDCustomers", newName: "Customers");
            RenameTable(name: "dbo.BDCompounds", newName: "Compounds");
            RenameTable(name: "dbo.UserRoles", newName: "PMSRoles");
            RenameTable(name: "dbo.UserAccesses", newName: "PMSAccesses");
        }
    }
}
