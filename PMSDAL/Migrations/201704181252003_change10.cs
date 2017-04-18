namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryItems", "Dimension", c => c.String());
            AddColumn("dbo.DeliveryItems", "DimensionActual", c => c.String());
            AddColumn("dbo.DeliveryItems", "Defects", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryItems", "Defects");
            DropColumn("dbo.DeliveryItems", "DimensionActual");
            DropColumn("dbo.DeliveryItems", "Dimension");
        }
    }
}
