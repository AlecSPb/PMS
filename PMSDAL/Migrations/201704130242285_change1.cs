namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSMaterialInventoryIns", "PMINumber", c => c.String());
            AddColumn("dbo.PMSMaterialInventoryOuts", "PMINumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSMaterialInventoryOuts", "PMINumber");
            DropColumn("dbo.PMSMaterialInventoryIns", "PMINumber");
        }
    }
}
