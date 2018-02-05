namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteIsaque : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Maquinas");

            DropForeignKey("dbo.Maquinas", "Estabelecimentos_Id", "dbo.Estabelecimentos");
            DropIndex("dbo.Maquinas", new[] { "Estabelecimentos_Id" });
            RenameColumn(table: "dbo.Maquinas", name: "Estabelecimentos_Id", newName: "IdEstabelecimento");
            AlterColumn("dbo.Maquinas", "IdEstabelecimento", c => c.Int(nullable: false));
            CreateIndex("dbo.Maquinas", "IdEstabelecimento");
            AddForeignKey("dbo.Maquinas", "IdEstabelecimento", "dbo.Estabelecimentos", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Maquinas", "IdEstabelecimento", "dbo.Estabelecimentos");
            DropIndex("dbo.Maquinas", new[] { "IdEstabelecimento" });
            AlterColumn("dbo.Maquinas", "IdEstabelecimento", c => c.Int());
            RenameColumn(table: "dbo.Maquinas", name: "IdEstabelecimento", newName: "Estabelecimentos_Id");
            CreateIndex("dbo.Maquinas", "Estabelecimentos_Id");
            AddForeignKey("dbo.Maquinas", "Estabelecimentos_Id", "dbo.Estabelecimentos", "Id");
        }
    }
}
