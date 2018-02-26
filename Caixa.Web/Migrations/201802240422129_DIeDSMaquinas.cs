namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DIeDSMaquinas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Maquinas", "DI", c => c.Single(nullable: false));
            AddColumn("dbo.Maquinas", "DS", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Maquinas", "DS");
            DropColumn("dbo.Maquinas", "DI");
        }
    }
}
