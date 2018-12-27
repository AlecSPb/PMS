namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change69 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialNeedHistories", "HowManyTargets", c => c.String());
            AddColumn("dbo.MaterialNeeds", "HowManyTargets", c => c.String());
            AddColumn("dbo.MaterialOrderItemHistories", "HowManyTargets", c => c.String());
            AddColumn("dbo.MaterialOrderItems", "HowManyTargets", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialOrderItems", "HowManyTargets");
            DropColumn("dbo.MaterialOrderItemHistories", "HowManyTargets");
            DropColumn("dbo.MaterialNeeds", "HowManyTargets");
            DropColumn("dbo.MaterialNeedHistories", "HowManyTargets");
        }
    }
}
