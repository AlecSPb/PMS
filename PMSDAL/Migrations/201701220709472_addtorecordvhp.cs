namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtorecordvhp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVHPs", "Composition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordVHPs", "Composition");
        }
    }
}
