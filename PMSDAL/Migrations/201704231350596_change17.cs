namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plates",
                c => new
                    {
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Plates");
        }
    }
}
