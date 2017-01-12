namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PMSAccesses", "PMSRole_ID", "dbo.PMSRoles");
            DropForeignKey("dbo.PMSUsers", "Role_ID", "dbo.PMSRoles");
            DropIndex("dbo.PMSAccesses", new[] { "PMSRole_ID" });
            DropIndex("dbo.PMSUsers", new[] { "Role_ID" });
            CreateTable(
                "dbo.PMSRolePMSAccesses",
                c => new
                    {
                        PMSRole_ID = c.Guid(nullable: false),
                        PMSAccess_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PMSRole_ID, t.PMSAccess_ID })
                .ForeignKey("dbo.PMSRoles", t => t.PMSRole_ID, cascadeDelete: true)
                .ForeignKey("dbo.PMSAccesses", t => t.PMSAccess_ID, cascadeDelete: true)
                .Index(t => t.PMSRole_ID)
                .Index(t => t.PMSAccess_ID);
            
            AddColumn("dbo.PMSUsers", "RoleID", c => c.Guid(nullable: false));
            DropColumn("dbo.PMSAccesses", "PMSRole_ID");
            DropColumn("dbo.PMSUsers", "Role_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PMSUsers", "Role_ID", c => c.Guid());
            AddColumn("dbo.PMSAccesses", "PMSRole_ID", c => c.Guid());
            DropForeignKey("dbo.PMSRolePMSAccesses", "PMSAccess_ID", "dbo.PMSAccesses");
            DropForeignKey("dbo.PMSRolePMSAccesses", "PMSRole_ID", "dbo.PMSRoles");
            DropIndex("dbo.PMSRolePMSAccesses", new[] { "PMSAccess_ID" });
            DropIndex("dbo.PMSRolePMSAccesses", new[] { "PMSRole_ID" });
            DropColumn("dbo.PMSUsers", "RoleID");
            DropTable("dbo.PMSRolePMSAccesses");
            CreateIndex("dbo.PMSUsers", "Role_ID");
            CreateIndex("dbo.PMSAccesses", "PMSRole_ID");
            AddForeignKey("dbo.PMSUsers", "Role_ID", "dbo.PMSRoles", "ID");
            AddForeignKey("dbo.PMSAccesses", "PMSRole_ID", "dbo.PMSRoles", "ID");
        }
    }
}
