namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegiaoIdToEstabelecimentos : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Estabelecimentos", name: "IdRegiao", newName: "RegiaoId");
            RenameIndex(table: "dbo.Estabelecimentos", name: "IX_IdRegiao", newName: "IX_RegiaoId");
            AddColumn("dbo.Regioes", "Nome", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Regioes", "Regiao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Regioes", "Regiao", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Regioes", "Nome");
            RenameIndex(table: "dbo.Estabelecimentos", name: "IX_RegiaoId", newName: "IX_IdRegiao");
            RenameColumn(table: "dbo.Estabelecimentos", name: "RegiaoId", newName: "IdRegiao");
        }
    }
}
