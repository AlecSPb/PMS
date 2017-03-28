namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepminumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMSMaterialNeeds", "PMINumber", c => c.String());
            AddColumn("dbo.PMSMaterialOrderItems", "PMINumber", c => c.String());
            DropColumn("dbo.PMSMaterialNeeds", "PMIWorkingNumber");
            DropColumn("dbo.PMSMaterialOrderItems", "PMIWorkNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PMSMaterialOrderItems", "PMIWorkNumber", c => c.String());
            AddColumn("dbo.PMSMaterialNeeds", "PMIWorkingNumber", c => c.String());
            DropColumn("dbo.PMSMaterialOrderItems", "PMINumber");
            DropColumn("dbo.PMSMaterialNeeds", "PMINumber");
        }
    }
}
