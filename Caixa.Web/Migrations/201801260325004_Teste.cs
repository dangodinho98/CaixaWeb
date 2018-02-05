namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Maquinas", "Id_Estabelecimentos_Id", "dbo.Estabelecimentos");
            DropIndex("dbo.Maquinas", new[] { "Id_Estabelecimentos_Id" });
            AlterColumn("dbo.Estabelecimentos", "Ativo", c => c.Boolean(nullable: false));
            DropColumn("dbo.Maquinas", "Id_Estabelecimentos_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Maquinas", "Id_Estabelecimentos_Id", c => c.Int());
            AlterColumn("dbo.Estabelecimentos", "Ativo", c => c.Boolean());
            CreateIndex("dbo.Maquinas", "Id_Estabelecimentos_Id");
            AddForeignKey("dbo.Maquinas", "Id_Estabelecimentos_Id", "dbo.Estabelecimentos", "Id");
        }
    }
}
