namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change88 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToolSieves",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SearchID = c.String(),
                        Manufacture = c.String(),
                        Specification = c.String(),
                        MaterialGroup = c.String(),
                        Remark = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        StopTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToolSieves");
        }
    }
}
