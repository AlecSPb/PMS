namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMSIndexes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IndexType = c.String(),
                        IndexValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.FeedBacks", "Problem", c => c.String());
            AddColumn("dbo.PlateHistories", "PrintNumber", c => c.String());
            AddColumn("dbo.Plates", "PrintNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plates", "PrintNumber");
            DropColumn("dbo.PlateHistories", "PrintNumber");
            DropColumn("dbo.FeedBacks", "Problem");
            DropTable("dbo.PMSIndexes");
        }
    }
}
