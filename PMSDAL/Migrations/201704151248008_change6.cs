namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondings", "TargetDimension", c => c.String());
            AddColumn("dbo.RecordBondings", "Defects", c => c.String());
            DropColumn("dbo.Products", "CompositionXRF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CompositionXRF", c => c.String());
            DropColumn("dbo.RecordBondings", "Defects");
            DropColumn("dbo.RecordBondings", "TargetDimension");
        }
    }
}
