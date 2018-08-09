namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondingHistories", "WeldingRate", c => c.Double(nullable: false));
            AddColumn("dbo.RecordBondings", "WeldingRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordBondings", "WeldingRate");
            DropColumn("dbo.RecordBondingHistories", "WeldingRate");
        }
    }
}
