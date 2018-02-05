namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Acertoes", newName: "Acerto");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Acerto", newName: "Acertoes");
        }
    }
}
