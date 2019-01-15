namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change70 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordTestHistories", "Warping", c => c.String());
            AddColumn("dbo.RecordTests", "Warping", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "Warping");
            DropColumn("dbo.RecordTestHistories", "Warping");
        }
    }
}
