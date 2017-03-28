namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmoreitemintoelement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BDElements", "Density", c => c.Double(nullable: false));
            AddColumn("dbo.BDElements", "MeltingPoint", c => c.String());
            AddColumn("dbo.BDElements", "BoilingPoint", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BDElements", "BoilingPoint");
            DropColumn("dbo.BDElements", "MeltingPoint");
            DropColumn("dbo.BDElements", "Density");
        }
    }
}
