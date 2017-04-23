namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlateHistories",
                c => new
                    {
                        HistoryID = c.Guid(nullable: false),
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        PlateLot = c.String(),
                        Dimension = c.String(),
                        Weight = c.String(),
                        Supplier = c.String(),
                        Appearance = c.String(),
                        Defects = c.String(),
                        Remark = c.String(),
                        Operator = c.String(),
                        OperateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlateHistories");
        }
    }
}
