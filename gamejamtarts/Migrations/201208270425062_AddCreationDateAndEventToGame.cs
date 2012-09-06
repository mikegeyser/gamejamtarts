namespace gamejamtarts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreationDateAndEventToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Event", c => c.String());
            AddColumn("dbo.Games", "CreationDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "CreationDate");
            DropColumn("dbo.Games", "Event");
        }
    }
}
