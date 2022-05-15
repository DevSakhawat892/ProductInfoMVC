namespace EmployeeInfoMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatNo = c.String(),
                        CatName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdName = c.String(nullable: false),
                        ProdCoded = c.String(),
                        ExpireDate = c.DateTime(nullable: false, storeType: "date"),
                        Supplier = c.String(),
                        Company = c.String(),
                        CategoryId = c.Int(nullable: false),
                        RackPosition = c.String(),
                        ProductType = c.String(),
                        AlertQuantity = c.String(),
                        PruchasePrice = c.String(),
                        SalesPrice = c.String(),
                        ProductImage = c.String(),
                        Stock = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
