namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change35 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanVHPConclusions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        PlanID = c.Guid(nullable: false),
                        Grade = c.Int(nullable: false),
                        Description = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Updator = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlanVHPConclusions");
        }
    }
}
