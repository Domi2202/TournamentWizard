namespace TournamentWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminationFinalUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EliminationFinals", "ParticipantCount", c => c.Int(nullable: false));
            AddColumn("dbo.EliminationFinals", "LastPlayoffPosition", c => c.Int(nullable: false));
            AddColumn("dbo.EliminationFinals", "Tier", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EliminationFinals", "Tier");
            DropColumn("dbo.EliminationFinals", "LastPlayoffPosition");
            DropColumn("dbo.EliminationFinals", "ParticipantCount");
        }
    }
}
