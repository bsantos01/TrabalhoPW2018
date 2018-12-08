namespace TrabalhoPW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialBuild : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aluguers",
                c => new
                    {
                        AluguerID = c.Int(nullable: false, identity: true),
                        ObjID = c.Int(nullable: false),
                        DataIncio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                        DataEntrega = c.DateTime(nullable: false),
                        Finalidade = c.String(nullable: false, maxLength: 50),
                        Validado = c.Boolean(nullable: false),
                        RequerenteID = c.Int(nullable: false),
                        EstadoI = c.Int(nullable: false),
                        EstadoF = c.Int(nullable: false),
                        Relatorio = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.AluguerID)
                .ForeignKey("dbo.Objetoes", t => t.ObjID, cascadeDelete: true)
                .ForeignKey("dbo.Utilizadors", t => t.RequerenteID, cascadeDelete: true)
                .Index(t => t.ObjID)
                .Index(t => t.RequerenteID);
            
            CreateTable(
                "dbo.Objetoes",
                c => new
                    {
                        ObjID = c.Int(nullable: false, identity: true),
                        Tipo = c.String(nullable: false, maxLength: 50),
                        Periodo = c.Int(nullable: false),
                        Zona = c.String(nullable: false, maxLength: 50),
                        Origem = c.Int(nullable: false),
                        Descricao = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ObjID);
            
            CreateTable(
                "dbo.Utilizadors",
                c => new
                    {
                        UtilizadorID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                        BI = c.Int(nullable: false),
                        NIF = c.Int(nullable: false),
                        Tipo = c.String(nullable: false),
                        Valido = c.Boolean(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UtilizadorID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Mensagems",
                c => new
                    {
                        MensagemID = c.Int(nullable: false, identity: true),
                        RemNome = c.String(maxLength: 150),
                        DestinatarioID = c.Int(nullable: false),
                        Conteudo = c.String(nullable: false, maxLength: 200),
                        data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MensagemID)
                .ForeignKey("dbo.Utilizadors", t => t.DestinatarioID, cascadeDelete: true)
                .Index(t => t.DestinatarioID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Tratamentoes",
                c => new
                    {
                        TratID = c.Int(nullable: false, identity: true),
                        Desc = c.String(nullable: false, maxLength: 200),
                        Data = c.DateTime(nullable: false),
                        ObjID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TratID)
                .ForeignKey("dbo.Objetoes", t => t.ObjID, cascadeDelete: true)
                .Index(t => t.ObjID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tratamentoes", "ObjID", "dbo.Objetoes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Mensagems", "DestinatarioID", "dbo.Utilizadors");
            DropForeignKey("dbo.Aluguers", "RequerenteID", "dbo.Utilizadors");
            DropForeignKey("dbo.Utilizadors", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Aluguers", "ObjID", "dbo.Objetoes");
            DropIndex("dbo.Tratamentoes", new[] { "ObjID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Mensagems", new[] { "DestinatarioID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Utilizadors", new[] { "UserID" });
            DropIndex("dbo.Aluguers", new[] { "RequerenteID" });
            DropIndex("dbo.Aluguers", new[] { "ObjID" });
            DropTable("dbo.Tratamentoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Mensagems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Utilizadors");
            DropTable("dbo.Objetoes");
            DropTable("dbo.Aluguers");
        }
    }
}
