namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change110 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccessGrants",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ControlName = c.String(),
                        RoleGroupString = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccessGrants");
        }
    }
}
