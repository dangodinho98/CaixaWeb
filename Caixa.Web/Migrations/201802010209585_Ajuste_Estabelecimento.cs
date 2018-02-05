namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajuste_Estabelecimento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estabelecimentos", "Telefone", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Estabelecimentos", "Observacao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Estabelecimentos", "Observacao");
            DropColumn("dbo.Estabelecimentos", "Telefone");
        }
    }
}
