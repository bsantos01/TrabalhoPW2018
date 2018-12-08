namespace TrabalhoPW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bugaluguer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Aluguers", "DataEntrega", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Aluguers", "DataEntrega", c => c.DateTime(nullable: false));
        }
    }
}
