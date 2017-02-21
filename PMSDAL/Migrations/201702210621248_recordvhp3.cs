namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recordvhp3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecordVHPs", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecordVHPs", "CreateTime", c => c.String());
        }
    }
}
