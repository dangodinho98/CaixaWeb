namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regioesFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Regioes", "estabelecimentos_Id", "dbo.Estabelecimentos");
            DropIndex("dbo.Regioes", new[] { "estabelecimentos_Id" });
            RenameColumn(table: "dbo.Regioes", name: "estabelecimentos_Id", newName: "IdEstabelecimento");
            AlterColumn("dbo.Regioes", "IdEstabelecimento", c => c.Int(nullable: false));
            CreateIndex("dbo.Regioes", "IdEstabelecimento");
            AddForeignKey("dbo.Regioes", "IdEstabelecimento", "dbo.Estabelecimentos", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regioes", "IdEstabelecimento", "dbo.Estabelecimentos");
            DropIndex("dbo.Regioes", new[] { "IdEstabelecimento" });
            AlterColumn("dbo.Regioes", "IdEstabelecimento", c => c.Int());
            RenameColumn(table: "dbo.Regioes", name: "IdEstabelecimento", newName: "estabelecimentos_Id");
            CreateIndex("dbo.Regioes", "estabelecimentos_Id");
            AddForeignKey("dbo.Regioes", "estabelecimentos_Id", "dbo.Estabelecimentos", "Id");
        }
    }
}
