namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change72 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMICounters",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ItemGroup = c.String(),
                        ItemName = c.String(),
                        ItemSpecification = c.String(),
                        ItemDetails = c.String(),
                        ItemCount = c.Double(nullable: false),
                        Unit = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PMICounters");
        }
    }
}
