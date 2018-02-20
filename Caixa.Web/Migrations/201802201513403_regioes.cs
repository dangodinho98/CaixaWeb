namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regioes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regioes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Regiao = c.String(nullable: false, maxLength: 255),
                        UserName = c.String(),
                        estabelecimentos_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estabelecimentos", t => t.estabelecimentos_Id)
                .Index(t => t.Regiao, unique: true)
                .Index(t => t.estabelecimentos_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regioes", "estabelecimentos_Id", "dbo.Estabelecimentos");
            DropIndex("dbo.Regioes", new[] { "estabelecimentos_Id" });
            DropIndex("dbo.Regioes", new[] { "Regiao" });
            DropTable("dbo.Regioes");
        }
    }
}
