namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eu : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Estabelecimentos", new[] { "Nome" });
            AlterColumn("dbo.Estabelecimentos", "Nome", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Estabelecimentos", "Nome", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Estabelecimentos", new[] { "Nome" });
            AlterColumn("dbo.Estabelecimentos", "Nome", c => c.String(maxLength: 255));
            CreateIndex("dbo.Estabelecimentos", "Nome", unique: true);
        }
    }
}
