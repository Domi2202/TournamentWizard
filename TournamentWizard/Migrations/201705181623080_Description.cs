namespace TournamentWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SportEvents", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SportEvents", "Description");
        }
    }
}
