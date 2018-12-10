namespace TrabalhoPW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feed1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilizadors", "Feedback", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizadors", "Feedback", c => c.Single(nullable: false));
        }
    }
}
