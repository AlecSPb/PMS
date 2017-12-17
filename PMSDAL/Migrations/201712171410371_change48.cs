namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialOrderItemHistories", "SJIngredient", c => c.String());
            AddColumn("dbo.MaterialOrderItems", "SJIngredient", c => c.String());
            AddColumn("dbo.RecordMillingHistories", "MeltingPoint", c => c.String());
            AddColumn("dbo.RecordMillings", "MeltingPoint", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecordMillings", "MeltingPoint");
            DropColumn("dbo.RecordMillingHistories", "MeltingPoint");
            DropColumn("dbo.MaterialOrderItems", "SJIngredient");
            DropColumn("dbo.MaterialOrderItemHistories", "SJIngredient");
        }
    }
}
