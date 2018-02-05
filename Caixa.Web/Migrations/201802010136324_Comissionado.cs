namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comissionado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comissionado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Telefone = c.String(nullable: false, maxLength: 15),
                        CPF = c.String(nullable: false, maxLength: 11),
                        Comissao = c.Single(nullable: false),
                        GanhoAcumulado = c.Single(),
                        Bloqueado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comissionado");
        }
    }
}
