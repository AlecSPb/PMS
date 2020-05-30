namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change128 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BDCustomers", "ImportanceLevel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BDCustomers", "ImportanceLevel");
        }
    }
}
