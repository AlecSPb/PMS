namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change126 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordBondingHistories", "WeldingDefect", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldingDefect", c => c.String());
            AddColumn("dbo.ToolMillings", "BoxNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToolMillings", "BoxNumber");
            DropColumn("dbo.RecordBondings", "WeldingDefect");
            DropColumn("dbo.RecordBondingHistories", "WeldingDefect");
        }
    }
}
