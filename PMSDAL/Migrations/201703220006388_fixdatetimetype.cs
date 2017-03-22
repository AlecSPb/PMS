namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdatetimetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecordBondingPlates", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RecordBondings", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RecordBondingTargets", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RecordDeMolds", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RecordMachines", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecordMachines", "CreateTime", c => c.String());
            AlterColumn("dbo.RecordDeMolds", "CreateTime", c => c.String());
            AlterColumn("dbo.RecordBondingTargets", "CreateTime", c => c.String());
            AlterColumn("dbo.RecordBondings", "CreateTime", c => c.String());
            AlterColumn("dbo.RecordBondingPlates", "CreateTime", c => c.String());
        }
    }
}
