namespace Products.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoIdCategoria : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produto", "CategoryId", "dbo.Categoria");
            DropIndex("dbo.Produto", new[] { "CategoryId" });
            DropPrimaryKey("dbo.Categoria");
            AddColumn("dbo.Produto", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Categoria", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categoria", "Id");
            CreateIndex("dbo.Produto", "Category_Id");
            AddForeignKey("dbo.Produto", "Category_Id", "dbo.Categoria", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produto", "Category_Id", "dbo.Categoria");
            DropIndex("dbo.Produto", new[] { "Category_Id" });
            DropPrimaryKey("dbo.Categoria");
            AlterColumn("dbo.Categoria", "Id", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Produto", "Category_Id");
            AddPrimaryKey("dbo.Categoria", "Id");
            CreateIndex("dbo.Produto", "CategoryId");
            AddForeignKey("dbo.Produto", "CategoryId", "dbo.Categoria", "Id", cascadeDelete: true);
        }
    }
}
