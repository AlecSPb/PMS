namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change47 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskPlans",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        PersonCharge = c.String(),
                        DeadLine = c.DateTime(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RecordMillingHistories", "Oxygen", c => c.String());
            AddColumn("dbo.RecordMillingHistories", "Water", c => c.String());
            AddColumn("dbo.RecordMillings", "Oxygen", c => c.String());
            AddColumn("dbo.RecordMillings", "Water", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "Water");
            DropColumn("dbo.RecordMillings", "Oxygen");
            DropColumn("dbo.RecordMillingHistories", "Water");
            DropColumn("dbo.RecordMillingHistories", "Oxygen");
            DropTable("dbo.TaskPlans");
        }
    }
}
