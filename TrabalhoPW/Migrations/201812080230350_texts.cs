namespace TrabalhoPW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class texts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Texts",
                c => new
                    {
                        TextID = c.Int(nullable: false, identity: true),
                        Pagina = c.String(maxLength: 50),
                        SubT = c.String(maxLength: 150),
                        Conteudo = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.TextID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Texts");
        }
    }
}
