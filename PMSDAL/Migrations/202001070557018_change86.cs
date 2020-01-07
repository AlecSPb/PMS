namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change86 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditLocks",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Operator = c.String(),
                        LockTime = c.String(),
                        ComputerInfo = c.String(),
                        ItemName = c.String(),
                        FingerPrint = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EditLocks");
        }
    }
}
