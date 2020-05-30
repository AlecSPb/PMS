namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change129 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BDCustomers", "ImportanceLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BDCustomers", "ImportanceLevel", c => c.String());
        }
    }
}
