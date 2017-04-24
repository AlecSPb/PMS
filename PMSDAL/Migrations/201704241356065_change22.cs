namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plates", "PlateMaterial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plates", "PlateMaterial");
        }
    }
}
