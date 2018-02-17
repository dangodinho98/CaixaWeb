namespace Caixa.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securitySQL : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Securities", newName: "Security");
            Sql("INSERT INTO Security(UserName,Level) VALUES('dangodinho98@gmail.com',5)");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.Security", newName: "Securities");
        }
    }
}
