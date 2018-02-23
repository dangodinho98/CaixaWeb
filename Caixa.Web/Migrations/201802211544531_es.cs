namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class es : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Regioes", "IdEstabelecimento", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Regioes", "IdEstabelecimento");
        }
    }
}
