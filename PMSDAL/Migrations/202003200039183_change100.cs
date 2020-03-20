namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drawings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DrawingName = c.String(),
                        DrawingType = c.String(),
                        Customer = c.String(),
                        MainDimension = c.String(),
                        ExtraDimension = c.String(),
                        Remark = c.String(),
                        FileName = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Drawings");
        }
    }
}
