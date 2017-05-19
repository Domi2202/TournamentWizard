namespace TournamentWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Competitions", "MatchDuration", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Competitions", "MatchDuration");
        }
    }
}
