namespace DeepChecks.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Relationship", "OwnerId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Note", "NoteTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Relationship", "RelationshipName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Relationship", "RelationshipName", c => c.String());
            AlterColumn("dbo.Note", "NoteTitle", c => c.String());
            DropColumn("dbo.Relationship", "OwnerId");
            DropColumn("dbo.Note", "OwnerId");
        }
    }
}
