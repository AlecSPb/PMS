namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change131 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlateHistories", "Parallelism", c => c.String());
            AddColumn("dbo.PlateHistories", "DimensionActual", c => c.String());
            AddColumn("dbo.Plates", "Parallelism", c => c.String());
            AddColumn("dbo.Plates", "DimensionActual", c => c.String());
            AddColumn("dbo.RecordTestHistories", "Parallelism", c => c.String());
            AddColumn("dbo.RecordTests", "Parallelism", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordTests", "Parallelism");
            DropColumn("dbo.RecordTestHistories", "Parallelism");
            DropColumn("dbo.Plates", "DimensionActual");
            DropColumn("dbo.Plates", "Parallelism");
            DropColumn("dbo.PlateHistories", "DimensionActual");
            DropColumn("dbo.PlateHistories", "Parallelism");
        }
    }
}
