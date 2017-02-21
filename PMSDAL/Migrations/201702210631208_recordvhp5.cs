namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recordvhp5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecordVHPs", "MoldDiameter", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecordVHPs", "MoldDiameter", c => c.String());
        }
    }
}
