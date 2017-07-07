namespace NEEI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Preco", c => c.Single(nullable: false));
            DropColumn("dbo.Items", "Preço");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Preço", c => c.Single(nullable: false));
            DropColumn("dbo.Items", "Preco");
        }
    }
}
