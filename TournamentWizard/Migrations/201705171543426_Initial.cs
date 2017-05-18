namespace TournamentWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competitions",
                c => new
                    {
                        CompetitionID = c.Int(nullable: false, identity: true),
                        CompetitionName = c.String(),
                        SportEventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompetitionID)
                .ForeignKey("dbo.SportEvents", t => t.SportEventID, cascadeDelete: true)
                .Index(t => t.SportEventID);
            
            CreateTable(
                "dbo.SportEvents",
                c => new
                    {
                        SportEventID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FieldCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SportEventID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        LateStart = c.Boolean(nullable: false),
                        CompetitionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.Competitions", t => t.CompetitionID, cascadeDelete: true)
                .Index(t => t.CompetitionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "CompetitionID", "dbo.Competitions");
            DropForeignKey("dbo.Competitions", "SportEventID", "dbo.SportEvents");
            DropIndex("dbo.Teams", new[] { "CompetitionID" });
            DropIndex("dbo.Competitions", new[] { "SportEventID" });
            DropTable("dbo.Teams");
            DropTable("dbo.SportEvents");
            DropTable("dbo.Competitions");
        }
    }
}
