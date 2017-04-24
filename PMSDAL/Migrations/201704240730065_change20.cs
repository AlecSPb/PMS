namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordDeMoldHistories", "CalculateDimension", c => c.String());
            AddColumn("dbo.RecordDeMolds", "CalculateDimension", c => c.String());
            DropColumn("dbo.RecordDeMoldHistories", "BlankDimension");
            DropColumn("dbo.RecordDeMolds", "BlankDimension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordDeMolds", "BlankDimension", c => c.String());
            AddColumn("dbo.RecordDeMoldHistories", "BlankDimension", c => c.String());
            DropColumn("dbo.RecordDeMolds", "CalculateDimension");
            DropColumn("dbo.RecordDeMoldHistories", "CalculateDimension");
        }
    }
}
