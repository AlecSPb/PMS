namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addelemententity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BDElements",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        AtomicNumber = c.Int(nullable: false),
                        MolWeight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BDSieves",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SieveName = c.String(),
                        Mesh = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        Manufacuture = c.String(),
                        EstimateUsedCount = c.Int(nullable: false),
                        CurrentCount = c.Int(nullable: false),
                        State = c.String(),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BDSieves");
            DropTable("dbo.BDElements");
        }
    }
}
