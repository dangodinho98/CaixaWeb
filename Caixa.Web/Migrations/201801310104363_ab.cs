namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ab : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Acertoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Entrada = c.Single(nullable: false),
                        Saida = c.Single(nullable: false),
                        Subtotal = c.Single(),
                        Comissao = c.Single(),
                        Quebra = c.Single(),
                        Despesa = c.Single(),
                        Total = c.Single(),
                        Data = c.DateTime(nullable: false),
                        Estabelecimento_Id = c.Int(),
                        Maquina_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estabelecimentos", t => t.Estabelecimento_Id)
                .ForeignKey("dbo.Maquinas", t => t.Maquina_Id)
                .Index(t => t.Estabelecimento_Id)
                .Index(t => t.Maquina_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acertoes", "Maquina_Id", "dbo.Maquinas");
            DropForeignKey("dbo.Acertoes", "Estabelecimento_Id", "dbo.Estabelecimentos");
            DropIndex("dbo.Acertoes", new[] { "Maquina_Id" });
            DropIndex("dbo.Acertoes", new[] { "Estabelecimento_Id" });
            DropTable("dbo.Acertoes");
        }
    }
}
