namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change75 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OutsideProcesses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProductID = c.String(),
                        Composition = c.String(),
                        Dimension = c.String(),
                        PMINumber = c.String(),
                        PONumber = c.String(),
                        Customer = c.String(),
                        Processor = c.String(),
                        ProgressBar = c.String(),
                        Remark = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OutsideProcesses");
        }
    }
}
