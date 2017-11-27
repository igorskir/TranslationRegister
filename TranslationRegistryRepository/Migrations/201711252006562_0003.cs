namespace SqlRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0003 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "User_Id", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "User_Id" });
            RenameColumn(table: "dbo.Documents", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Documents", name: "DocumentLanguage_Id", newName: "LanguageId");
            RenameIndex(table: "dbo.Documents", name: "IX_DocumentLanguage_Id", newName: "IX_LanguageId");
            AlterColumn("dbo.Documents", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Documents", "UserId");
            AddForeignKey("dbo.Documents", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "UserId" });
            AlterColumn("dbo.Documents", "UserId", c => c.Int());
            RenameIndex(table: "dbo.Documents", name: "IX_LanguageId", newName: "IX_DocumentLanguage_Id");
            RenameColumn(table: "dbo.Documents", name: "LanguageId", newName: "DocumentLanguage_Id");
            RenameColumn(table: "dbo.Documents", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Documents", "User_Id");
            AddForeignKey("dbo.Documents", "User_Id", "dbo.Users", "Id");
        }
    }
}
