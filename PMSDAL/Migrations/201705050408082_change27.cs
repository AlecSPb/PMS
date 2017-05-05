namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductHistories", "Remark", c => c.String());
            AddColumn("dbo.Products", "Remark", c => c.String());
            DropColumn("dbo.ProductHistories", "DetailRecord");
            DropColumn("dbo.Products", "DetailRecord");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "DetailRecord", c => c.String());
            AddColumn("dbo.ProductHistories", "DetailRecord", c => c.String());
            DropColumn("dbo.Products", "Remark");
            DropColumn("dbo.ProductHistories", "Remark");
        }
    }
}
