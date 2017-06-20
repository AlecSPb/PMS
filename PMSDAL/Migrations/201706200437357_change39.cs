namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSPlanVHPs", "Grade", c => c.Int(nullable: false));
            AddColumn("dbo.PMSPlanVHPs", "Conclusion", c => c.String());
            AddColumn("dbo.PMSPlanVHPs", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSPlanVHPs", "Updator", c => c.String());
            AddColumn("dbo.PMSPlanVHPHistories", "Grade", c => c.Int(nullable: false));
            AddColumn("dbo.PMSPlanVHPHistories", "Conclusion", c => c.String());
            AddColumn("dbo.PMSPlanVHPHistories", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PMSPlanVHPHistories", "Updator", c => c.String());
            DropTable("dbo.PlanVHPConclusions");
        }
        
        public override void Down()
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
            
            DropColumn("dbo.PMSPlanVHPHistories", "Updator");
            DropColumn("dbo.PMSPlanVHPHistories", "UpdateTime");
            DropColumn("dbo.PMSPlanVHPHistories", "Conclusion");
            DropColumn("dbo.PMSPlanVHPHistories", "Grade");
            DropColumn("dbo.PMSPlanVHPs", "Updator");
            DropColumn("dbo.PMSPlanVHPs", "UpdateTime");
            DropColumn("dbo.PMSPlanVHPs", "Conclusion");
            DropColumn("dbo.PMSPlanVHPs", "Grade");
        }
    }
}
