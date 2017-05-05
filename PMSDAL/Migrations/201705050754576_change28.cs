namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondingHistories", "CoverPlateNumber", c => c.String());
            AddColumn("dbo.RecordBondings", "CoverPlateNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordBondings", "CoverPlateNumber");
            DropColumn("dbo.RecordBondingHistories", "CoverPlateNumber");
        }
    }
}
