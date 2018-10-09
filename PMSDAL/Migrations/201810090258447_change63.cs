namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change63 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Failures",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Creator = c.String(),
                        State = c.String(),
                        ProductID = c.String(),
                        Details = c.String(),
                        Stage = c.String(),
                        Problem = c.String(),
                        Process = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Purchases", "NeedDepartment", c => c.String());
            DropColumn("dbo.Purchases", "NeedPerson");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "NeedPerson", c => c.String());
            DropColumn("dbo.Purchases", "NeedDepartment");
            DropTable("dbo.Failures");
        }
    }
}
