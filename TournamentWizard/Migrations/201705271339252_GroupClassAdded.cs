namespace TournamentWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupClassAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        CompetitionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Competitions", t => t.CompetitionID, cascadeDelete: true)
                .Index(t => t.CompetitionID);
            
            AddColumn("dbo.Teams", "GroupID", c => c.Int(nullable: true));
            CreateIndex("dbo.Teams", "GroupID");
            AddForeignKey("dbo.Teams", "GroupID", "dbo.Groups", "GroupID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "CompetitionID", "dbo.Competitions");
            DropIndex("dbo.Groups", new[] { "CompetitionID" });
            DropIndex("dbo.Teams", new[] { "GroupID" });
            DropColumn("dbo.Teams", "GroupID");
            DropTable("dbo.Groups");
        }
    }
}
