namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change49 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnvironmentInfoes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Position = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Temperature = c.Double(nullable: false),
                        Humidity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.BDCompounds", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BDCompounds", "Remark");
            DropTable("dbo.EnvironmentInfoes");
        }
    }
}
