namespace TournamentWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShouldBeEmpty1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EliminationFinals",
                c => new
                    {
                        EliminationFinalID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CompetitionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EliminationFinalID)
                .ForeignKey("dbo.Competitions", t => t.CompetitionID, cascadeDelete: true)
                .Index(t => t.CompetitionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EliminationFinals", "CompetitionID", "dbo.Competitions");
            DropIndex("dbo.EliminationFinals", new[] { "CompetitionID" });
            DropTable("dbo.EliminationFinals");
        }
    }
}
