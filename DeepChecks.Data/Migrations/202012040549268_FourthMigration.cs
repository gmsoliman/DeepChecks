namespace DeepChecks.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category", "Check_CheckId", "dbo.Check");
            DropForeignKey("dbo.Relationship", "CheckId", "dbo.Check");
            DropForeignKey("dbo.Relationship", "ParticipantId", "dbo.Participant");
            DropIndex("dbo.Category", new[] { "Check_CheckId" });
            DropIndex("dbo.Relationship", new[] { "ParticipantId" });
            DropIndex("dbo.Relationship", new[] { "CheckId" });
            CreateTable(
                "dbo.RelationshipParticipant",
                c => new
                    {
                        Relationship_RelationshipId = c.Int(nullable: false),
                        Participant_ParticipantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Relationship_RelationshipId, t.Participant_ParticipantId })
                .ForeignKey("dbo.Relationship", t => t.Relationship_RelationshipId, cascadeDelete: true)
                .ForeignKey("dbo.Participant", t => t.Participant_ParticipantId, cascadeDelete: true)
                .Index(t => t.Relationship_RelationshipId)
                .Index(t => t.Participant_ParticipantId);
            
            AddColumn("dbo.Check", "RelationshipId", c => c.Int(nullable: false));
            CreateIndex("dbo.Check", "RelationshipId");
            AddForeignKey("dbo.Check", "RelationshipId", "dbo.Relationship", "RelationshipId", cascadeDelete: true);
            DropColumn("dbo.Category", "Check_CheckId");
            DropColumn("dbo.Relationship", "ParticipantId");
            DropColumn("dbo.Relationship", "CheckId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Relationship", "CheckId", c => c.Int(nullable: false));
            AddColumn("dbo.Relationship", "ParticipantId", c => c.Int(nullable: false));
            AddColumn("dbo.Category", "Check_CheckId", c => c.Int());
            DropForeignKey("dbo.Check", "RelationshipId", "dbo.Relationship");
            DropForeignKey("dbo.RelationshipParticipant", "Participant_ParticipantId", "dbo.Participant");
            DropForeignKey("dbo.RelationshipParticipant", "Relationship_RelationshipId", "dbo.Relationship");
            DropIndex("dbo.RelationshipParticipant", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.RelationshipParticipant", new[] { "Relationship_RelationshipId" });
            DropIndex("dbo.Check", new[] { "RelationshipId" });
            DropColumn("dbo.Check", "RelationshipId");
            DropTable("dbo.RelationshipParticipant");
            CreateIndex("dbo.Relationship", "CheckId");
            CreateIndex("dbo.Relationship", "ParticipantId");
            CreateIndex("dbo.Category", "Check_CheckId");
            AddForeignKey("dbo.Relationship", "ParticipantId", "dbo.Participant", "ParticipantId", cascadeDelete: true);
            AddForeignKey("dbo.Relationship", "CheckId", "dbo.Check", "CheckId", cascadeDelete: true);
            AddForeignKey("dbo.Category", "Check_CheckId", "dbo.Check", "CheckId");
        }
    }
}
