namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Acerto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Acerto", "Estabelecimento_Id", "dbo.Estabelecimentos");
            DropForeignKey("dbo.Acerto", "Maquina_Id", "dbo.Maquinas");
            DropIndex("dbo.Acerto", new[] { "Estabelecimento_Id" });
            DropIndex("dbo.Acerto", new[] { "Maquina_Id" });
            RenameColumn(table: "dbo.Acerto", name: "Estabelecimento_Id", newName: "IdEstabelecimento");
            RenameColumn(table: "dbo.Acerto", name: "Maquina_Id", newName: "IdMaquina");
            AlterColumn("dbo.Acerto", "IdEstabelecimento", c => c.Int(nullable: false));
            AlterColumn("dbo.Acerto", "IdMaquina", c => c.Int(nullable: false));
            CreateIndex("dbo.Acerto", "IdMaquina");
            CreateIndex("dbo.Acerto", "IdEstabelecimento");
            AddForeignKey("dbo.Acerto", "IdEstabelecimento", "dbo.Estabelecimentos", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Acerto", "IdMaquina", "dbo.Maquinas", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acerto", "IdMaquina", "dbo.Maquinas");
            DropForeignKey("dbo.Acerto", "IdEstabelecimento", "dbo.Estabelecimentos");
            DropIndex("dbo.Acerto", new[] { "IdEstabelecimento" });
            DropIndex("dbo.Acerto", new[] { "IdMaquina" });
            AlterColumn("dbo.Acerto", "IdMaquina", c => c.Int());
            AlterColumn("dbo.Acerto", "IdEstabelecimento", c => c.Int());
            RenameColumn(table: "dbo.Acerto", name: "IdMaquina", newName: "Maquina_Id");
            RenameColumn(table: "dbo.Acerto", name: "IdEstabelecimento", newName: "Estabelecimento_Id");
            CreateIndex("dbo.Acerto", "Maquina_Id");
            CreateIndex("dbo.Acerto", "Estabelecimento_Id");
            AddForeignKey("dbo.Acerto", "Maquina_Id", "dbo.Maquinas", "Id");
            AddForeignKey("dbo.Acerto", "Estabelecimento_Id", "dbo.Estabelecimentos", "Id");
        }
    }
}
