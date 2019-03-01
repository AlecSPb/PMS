namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change73 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RemainInventories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductID = c.String(),
                        Composition = c.String(),
                        Dimension = c.String(),
                        Details = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RemainInventories");
        }
    }
}
