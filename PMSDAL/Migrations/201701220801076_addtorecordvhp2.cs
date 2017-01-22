namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtorecordvhp2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVHPs", "MoldCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordVHPs", "MoldCode");
        }
    }
}
