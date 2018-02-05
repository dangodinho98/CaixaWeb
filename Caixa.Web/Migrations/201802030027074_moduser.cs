namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moduser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estabelecimentos", "ModUser", c => c.String());
            AddColumn("dbo.Maquinas", "ModUser", c => c.String());
            AddColumn("dbo.Comissionado", "ModUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comissionado", "ModUser");
            DropColumn("dbo.Maquinas", "ModUser");
            DropColumn("dbo.Estabelecimentos", "ModUser");
        }
    }
}
