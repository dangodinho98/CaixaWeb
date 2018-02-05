namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estabelecimentos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 255),
                        Endereco = c.String(),
                        Regiao = c.String(),
                        Ativo = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Nome, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Estabelecimentos", new[] { "Nome" });
            DropTable("dbo.Estabelecimentos");
        }
    }
}
