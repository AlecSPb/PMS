namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change42 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondingHistories", "PlateType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordBondingHistories", "PlateType");
        }
    }
}
