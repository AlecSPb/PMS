namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgroupelement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BDElementGroupItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupElementID = c.Guid(nullable: false),
                        Name = c.String(),
                        GroupNumber = c.Int(nullable: false),
                        MolWeight = c.Double(nullable: false),
                        At = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BDElementGroups",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        GroupName = c.String(),
                        CreateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BDElementGroups");
            DropTable("dbo.BDElementGroupItems");
        }
    }
}
