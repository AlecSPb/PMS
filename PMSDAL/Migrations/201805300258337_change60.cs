namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change60 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToolFillings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        ToolNumber = c.Int(nullable: false),
                        CompositionAbbr = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ToolMillings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        ToolNumber = c.Int(nullable: false),
                        CompositionAbbr = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToolMillings");
            DropTable("dbo.ToolFillings");
        }
    }
}
