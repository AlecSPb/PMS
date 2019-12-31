namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change85 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MachineFixes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FixType = c.String(),
                        DeviceName = c.String(),
                        PartName = c.String(),
                        EventDescription = c.String(),
                        FixMeasure = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MachineFixes");
        }
    }
}
