namespace DeepChecks.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Check", "RelationshipId", "dbo.Relationship");
            DropIndex("dbo.Check", new[] { "RelationshipId" });
            DropPrimaryKey("dbo.Relationship");
            CreateTable(
                "dbo.Participant",
                c => new
                    {
                        ParticipantId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ParticipantId);
            
            AddColumn("dbo.Note", "ParticipantId", c => c.Int(nullable: false));
            AddColumn("dbo.Relationship", "ParticipantId", c => c.Int(nullable: false));
            AddColumn("dbo.Relationship", "CheckId", c => c.Int(nullable: false));
            AddColumn("dbo.Relationship", "RelationshipName", c => c.String());
            AddColumn("dbo.Entry", "ParticipantId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Relationship", new[] { "ParticipantId", "CheckId" });
            CreateIndex("dbo.Note", "ParticipantId");
            CreateIndex("dbo.Entry", "ParticipantId");
            CreateIndex("dbo.Relationship", "ParticipantId");
            CreateIndex("dbo.Relationship", "CheckId");
            AddForeignKey("dbo.Entry", "ParticipantId", "dbo.Participant", "ParticipantId", cascadeDelete: true);
            AddForeignKey("dbo.Relationship", "CheckId", "dbo.Check", "CheckId", cascadeDelete: true);
            AddForeignKey("dbo.Relationship", "ParticipantId", "dbo.Participant", "ParticipantId", cascadeDelete: true);
            AddForeignKey("dbo.Note", "ParticipantId", "dbo.Participant", "ParticipantId", cascadeDelete: true);
            DropColumn("dbo.Check", "RelationshipId");
            DropColumn("dbo.Relationship", "RelationshipId");
            DropColumn("dbo.Relationship", "Name");
            DropColumn("dbo.Relationship", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Relationship", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Relationship", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Relationship", "RelationshipId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Check", "RelationshipId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Note", "ParticipantId", "dbo.Participant");
            DropForeignKey("dbo.Relationship", "ParticipantId", "dbo.Participant");
            DropForeignKey("dbo.Relationship", "CheckId", "dbo.Check");
            DropForeignKey("dbo.Entry", "ParticipantId", "dbo.Participant");
            DropIndex("dbo.Relationship", new[] { "CheckId" });
            DropIndex("dbo.Relationship", new[] { "ParticipantId" });
            DropIndex("dbo.Entry", new[] { "ParticipantId" });
            DropIndex("dbo.Note", new[] { "ParticipantId" });
            DropPrimaryKey("dbo.Relationship");
            DropColumn("dbo.Entry", "ParticipantId");
            DropColumn("dbo.Relationship", "RelationshipName");
            DropColumn("dbo.Relationship", "CheckId");
            DropColumn("dbo.Relationship", "ParticipantId");
            DropColumn("dbo.Note", "ParticipantId");
            DropTable("dbo.Participant");
            AddPrimaryKey("dbo.Relationship", "RelationshipId");
            CreateIndex("dbo.Check", "RelationshipId");
            AddForeignKey("dbo.Check", "RelationshipId", "dbo.Relationship", "RelationshipId", cascadeDelete: true);
        }
    }
}
