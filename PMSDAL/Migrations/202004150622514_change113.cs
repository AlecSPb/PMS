namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change113 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RawMaterialSheets", "SampleRemark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RawMaterialSheets", "SampleRemark");
        }
    }
}
