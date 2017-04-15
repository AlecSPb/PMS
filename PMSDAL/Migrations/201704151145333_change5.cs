namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Dimension", c => c.String());
            AddColumn("dbo.Products", "CompositionXRF", c => c.String());
            AddColumn("dbo.Products", "DimensionActual", c => c.String());
            AddColumn("dbo.Products", "Defects", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Defects");
            DropColumn("dbo.Products", "DimensionActual");
            DropColumn("dbo.Products", "CompositionXRF");
            DropColumn("dbo.Products", "Dimension");
        }
    }
}
