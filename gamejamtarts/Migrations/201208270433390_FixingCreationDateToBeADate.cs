namespace gamejamtarts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingCreationDateToBeADate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "CreationDate", c => c.String());
        }
    }
}
