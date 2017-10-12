namespace SailorWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wage",
                c => new
                    {
                        WageID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        MonthlyDays = c.Int(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        WorkDays = c.Int(nullable: false),
                        StandardWage = c.Double(),
                        ShouldWage = c.Double(),
                        ContractID = c.Int(nullable: false),
                        SailorID = c.Int(nullable: false),
                        SailorName = c.String(maxLength: 10),
                        SysUserId = c.String(),
                        SysCompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WageID)
                .ForeignKey("dbo.Contract", t => t.ContractID, cascadeDelete: true)
                .ForeignKey("dbo.Sailor", t => t.SailorID, cascadeDelete: false)
                .Index(t => t.ContractID)
                .Index(t => t.SailorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wage", "SailorID", "dbo.Sailor");
            DropForeignKey("dbo.Wage", "ContractID", "dbo.Contract");
            DropIndex("dbo.Wage", new[] { "SailorID" });
            DropIndex("dbo.Wage", new[] { "ContractID" });
            DropTable("dbo.Wage");
        }
    }
}
