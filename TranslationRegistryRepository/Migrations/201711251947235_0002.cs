namespace SqlRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0002 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "User_Id", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "User_Id" });
            AlterColumn("dbo.Documents", "User_Id", c => c.Int());
            CreateIndex("dbo.Documents", "User_Id");
            AddForeignKey("dbo.Documents", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "User_Id", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "User_Id" });
            AlterColumn("dbo.Documents", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Documents", "User_Id");
            AddForeignKey("dbo.Documents", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
