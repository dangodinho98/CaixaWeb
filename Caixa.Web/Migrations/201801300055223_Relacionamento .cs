namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relacionamento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Maquinas", "Estabelecimentos_Id", c => c.Int());
            CreateIndex("dbo.Maquinas", "Estabelecimentos_Id");
            AddForeignKey("dbo.Maquinas", "Estabelecimentos_Id", "dbo.Estabelecimentos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Maquinas", "Estabelecimentos_Id", "dbo.Estabelecimentos");
            DropIndex("dbo.Maquinas", new[] { "Estabelecimentos_Id" });
            DropColumn("dbo.Maquinas", "Estabelecimentos_Id");
        }
    }
}
