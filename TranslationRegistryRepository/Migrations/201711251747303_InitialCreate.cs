namespace SqlRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(nullable: false),
                        DocumentLanguage_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.DocumentLanguage_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.DocumentLanguage_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShortName = c.String(nullable: false),
                        ShortName2323 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Documents", "DocumentLanguage_Id", "dbo.Languages");
            DropIndex("dbo.Documents", new[] { "User_Id" });
            DropIndex("dbo.Documents", new[] { "DocumentLanguage_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Languages");
            DropTable("dbo.Documents");
        }
    }
}
