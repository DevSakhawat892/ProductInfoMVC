namespace EmployeeInfoMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductImagePath", c => c.String());
            DropColumn("dbo.Products", "ProductImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductImage", c => c.String());
            DropColumn("dbo.Products", "ProductImagePath");
        }
    }
}
