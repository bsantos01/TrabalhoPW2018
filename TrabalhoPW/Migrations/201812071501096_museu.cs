namespace TrabalhoPW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class museu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Utilizadors", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Utilizadors", new[] { "UserID" });
            AddColumn("dbo.Utilizadors", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Utilizadors", "UserID", c => c.String());
            CreateIndex("dbo.Utilizadors", "ApplicationUser_Id");
            AddForeignKey("dbo.Utilizadors", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilizadors", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Utilizadors", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Utilizadors", "UserID", c => c.String(maxLength: 128));
            DropColumn("dbo.Utilizadors", "ApplicationUser_Id");
            CreateIndex("dbo.Utilizadors", "UserID");
            AddForeignKey("dbo.Utilizadors", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
