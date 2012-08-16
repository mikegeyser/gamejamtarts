namespace gamejamtarts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Code");
        }
    }
}
