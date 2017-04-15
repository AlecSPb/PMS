namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondings", "TargetDefects", c => c.String());
            DropColumn("dbo.RecordBondings", "Defects");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordBondings", "Defects", c => c.String());
            DropColumn("dbo.RecordBondings", "TargetDefects");
        }
    }
}
