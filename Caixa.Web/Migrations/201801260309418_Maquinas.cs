namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Maquinas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Maquinas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Tema = c.String(),
                        Lacre = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Id_Estabelecimentos_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estabelecimentos", t => t.Id_Estabelecimentos_Id)
                .Index(t => t.Codigo, unique: true)
                .Index(t => t.Lacre, unique: true)
                .Index(t => t.Id_Estabelecimentos_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Maquinas", "Id_Estabelecimentos_Id", "dbo.Estabelecimentos");
            DropIndex("dbo.Maquinas", new[] { "Id_Estabelecimentos_Id" });
            DropIndex("dbo.Maquinas", new[] { "Lacre" });
            DropIndex("dbo.Maquinas", new[] { "Codigo" });
            DropTable("dbo.Maquinas");
        }
    }
}
