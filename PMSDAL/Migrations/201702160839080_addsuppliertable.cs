namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsuppliertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BDSuppliers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SupplierName = c.String(),
                        Abbr = c.String(),
                        ContactPerson = c.String(),
                        CellPhone = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BDSuppliers");
        }
    }
}
