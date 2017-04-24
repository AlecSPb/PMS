namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlateHistories", "PlateMaterial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlateHistories", "PlateMaterial");
        }
    }
}
