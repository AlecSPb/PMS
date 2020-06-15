namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change132 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RawMaterialSheets", "Purity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RawMaterialSheets", "Purity");
        }
    }
}
