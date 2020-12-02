namespace DeepChecks.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Note", "NoteTitle", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Note", "NoteTitle", c => c.String(nullable: false));
        }
    }
}
