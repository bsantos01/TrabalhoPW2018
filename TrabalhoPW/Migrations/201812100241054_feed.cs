namespace TrabalhoPW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizadors", "Feedback", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilizadors", "Feedback");
        }
    }
}
