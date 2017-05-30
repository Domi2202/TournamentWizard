namespace TournamentWizard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "GroupID", "dbo.Groups");
            DropIndex("dbo.Teams", new[] { "GroupID" });
            AlterColumn("dbo.Teams", "GroupID", c => c.Int());
            CreateIndex("dbo.Teams", "GroupID");
            AddForeignKey("dbo.Teams", "GroupID", "dbo.Groups", "GroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "GroupID", "dbo.Groups");
            DropIndex("dbo.Teams", new[] { "GroupID" });
            AlterColumn("dbo.Teams", "GroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "GroupID");
            AddForeignKey("dbo.Teams", "GroupID", "dbo.Groups", "GroupID", cascadeDelete: true);
        }
    }
}
