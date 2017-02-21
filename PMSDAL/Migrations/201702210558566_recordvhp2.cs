namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recordvhp2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecordVHPs", "PlanID", c => c.Guid(nullable: false));
            AddColumn("dbo.RecordVHPs", "PlanDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.RecordVHPs", "VHPID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordVHPs", "VHPID", c => c.String());
            DropColumn("dbo.RecordVHPs", "PlanDate");
            DropColumn("dbo.RecordVHPs", "PlanID");
        }
    }
}
