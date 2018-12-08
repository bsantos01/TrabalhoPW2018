namespace TrabalhoPW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class museumcontext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mensagems", "data", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mensagems", "data", c => c.DateTime(nullable: false));
        }
    }
}
