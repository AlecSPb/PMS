namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class again : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondingPlates", "PlateSerialNumber", c => c.String());
            AddColumn("dbo.RecordBondingPlates", "PlateBelong", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordBondingPlates", "PlateBelong");
            DropColumn("dbo.RecordBondingPlates", "PlateSerialNumber");
        }
    }
}
