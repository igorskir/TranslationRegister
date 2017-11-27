namespace SqlRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0004 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "UserId" });
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        OriginalLanguageId = c.Int(),
                        FinalLanguageId = c.Int(),
                        WordsNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.FinalLanguageId)
                .ForeignKey("dbo.Languages", t => t.OriginalLanguageId)
                .Index(t => t.OriginalLanguageId)
                .Index(t => t.FinalLanguageId);
            
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Project_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Project_Id);
            
            AddColumn("dbo.Documents", "WordsNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Documents", "ProjectId", c => c.Int());
            AlterColumn("dbo.Documents", "UserId", c => c.Int());
            CreateIndex("dbo.Documents", "UserId");
            CreateIndex("dbo.Documents", "ProjectId");
            AddForeignKey("dbo.Documents", "ProjectId", "dbo.Projects", "Id");
            AddForeignKey("dbo.Documents", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Languages", "ShortName2323");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Languages", "ShortName2323", c => c.String());
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.UserProjects", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Projects", "OriginalLanguageId", "dbo.Languages");
            DropForeignKey("dbo.Projects", "FinalLanguageId", "dbo.Languages");
            DropForeignKey("dbo.Documents", "ProjectId", "dbo.Projects");
            DropIndex("dbo.UserProjects", new[] { "Project_Id" });
            DropIndex("dbo.UserProjects", new[] { "User_Id" });
            DropIndex("dbo.Projects", new[] { "FinalLanguageId" });
            DropIndex("dbo.Projects", new[] { "OriginalLanguageId" });
            DropIndex("dbo.Documents", new[] { "ProjectId" });
            DropIndex("dbo.Documents", new[] { "UserId" });
            AlterColumn("dbo.Documents", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Documents", "ProjectId");
            DropColumn("dbo.Documents", "WordsNumber");
            DropTable("dbo.UserProjects");
            DropTable("dbo.Projects");
            CreateIndex("dbo.Documents", "UserId");
            AddForeignKey("dbo.Documents", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
