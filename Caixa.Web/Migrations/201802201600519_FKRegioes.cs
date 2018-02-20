namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKRegioes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Regioes", "IdEstabelecimento", "dbo.Estabelecimentos");
            DropIndex("dbo.Regioes", new[] { "Regiao" });
            DropIndex("dbo.Regioes", new[] { "IdEstabelecimento" });
            AddColumn("dbo.Estabelecimentos", "Regioes_Id", c => c.Int());
            CreateIndex("dbo.Estabelecimentos", "Regioes_Id");
            AddForeignKey("dbo.Estabelecimentos", "Regioes_Id", "dbo.Regioes", "Id");
            DropColumn("dbo.Regioes", "IdEstabelecimento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Regioes", "IdEstabelecimento", c => c.Int(nullable: false));
            DropForeignKey("dbo.Estabelecimentos", "Regioes_Id", "dbo.Regioes");
            DropIndex("dbo.Estabelecimentos", new[] { "Regioes_Id" });
            DropColumn("dbo.Estabelecimentos", "Regioes_Id");
            CreateIndex("dbo.Regioes", "IdEstabelecimento");
            CreateIndex("dbo.Regioes", "Regiao", unique: true);
            AddForeignKey("dbo.Regioes", "IdEstabelecimento", "dbo.Estabelecimentos", "Id", cascadeDelete: true);
        }
    }
}
