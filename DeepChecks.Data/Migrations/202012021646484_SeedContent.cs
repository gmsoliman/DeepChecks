namespace DeepChecks.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedContent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category", "CheckId", "dbo.Check");
            DropIndex("dbo.Category", new[] { "CheckId" });
            RenameColumn(table: "dbo.Category", name: "CheckId", newName: "Check_CheckId");
            DropPrimaryKey("dbo.Relationship");
            AddColumn("dbo.Relationship", "RelationshipId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Category", "Check_CheckId", c => c.Int());
            AddPrimaryKey("dbo.Relationship", "RelationshipId");
            CreateIndex("dbo.Category", "Check_CheckId");
            AddForeignKey("dbo.Category", "Check_CheckId", "dbo.Check", "CheckId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "Check_CheckId", "dbo.Check");
            DropIndex("dbo.Category", new[] { "Check_CheckId" });
            DropPrimaryKey("dbo.Relationship");
            AlterColumn("dbo.Category", "Check_CheckId", c => c.Int(nullable: false));
            DropColumn("dbo.Relationship", "RelationshipId");
            AddPrimaryKey("dbo.Relationship", new[] { "ParticipantId", "CheckId" });
            RenameColumn(table: "dbo.Category", name: "Check_CheckId", newName: "CheckId");
            CreateIndex("dbo.Category", "CheckId");
            AddForeignKey("dbo.Category", "CheckId", "dbo.Check", "CheckId", cascadeDelete: true);
        }
    }
}
