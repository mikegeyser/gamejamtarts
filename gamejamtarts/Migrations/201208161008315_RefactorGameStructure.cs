namespace gamejamtarts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorGameStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Game_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.Game_ID)
                .Index(t => t.Game_ID);
            
            CreateTable(
                "dbo.GameFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Game_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.Game_ID, cascadeDelete: true)
                .Index(t => t.Game_ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Game_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.Game_ID, cascadeDelete: true)
                .Index(t => t.Game_ID);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "Game_ID" });
            DropIndex("dbo.GameFiles", new[] { "Game_ID" });
            DropIndex("dbo.People", new[] { "Game_ID" });
            DropForeignKey("dbo.Images", "Game_ID", "dbo.Games");
            DropForeignKey("dbo.GameFiles", "Game_ID", "dbo.Games");
            DropForeignKey("dbo.People", "Game_ID", "dbo.Games");
            DropTable("dbo.Images");
            DropTable("dbo.GameFiles");
            DropTable("dbo.People");
        }
    }
}
