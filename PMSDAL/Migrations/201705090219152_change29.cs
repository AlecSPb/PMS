namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change29 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckListHistories",
                c => new
                    {
                        HistoryID = c.Guid(nullable: false),
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Updator = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                        Operator = c.String(),
                        OperateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID);
            
            CreateTable(
                "dbo.CheckLists",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Updator = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        ProductID = c.String(),
                        ProductType = c.String(),
                        Composition = c.String(),
                        Customer = c.String(),
                        FeebackReason = c.String(),
                        ReceivedDate = c.String(),
                        ProcessWay = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemDebitHistories",
                c => new
                    {
                        HistoryID = c.Guid(nullable: false),
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        ItemType = c.String(),
                        ItemLot = c.String(),
                        ItemName = c.String(),
                        ItemProperty = c.String(),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Creditor = c.String(),
                        Remark = c.Double(nullable: false),
                        Operator = c.String(),
                        OperateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID);
            
            CreateTable(
                "dbo.ItemDebits",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                        ItemType = c.String(),
                        ItemLot = c.String(),
                        ItemName = c.String(),
                        ItemProperty = c.String(),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Creditor = c.String(),
                        Remark = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.MaintenancePlans", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MaintenanceRecords", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaintenanceRecords", "CreateTime", c => c.String());
            AlterColumn("dbo.MaintenancePlans", "CreateTime", c => c.String());
            DropTable("dbo.ItemDebits");
            DropTable("dbo.ItemDebitHistories");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.CheckLists");
            DropTable("dbo.CheckListHistories");
        }
    }
}
