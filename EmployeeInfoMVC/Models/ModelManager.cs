using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web;

namespace EmployeeInfoMVC.Models
{
   public class ModelManager : DbContext
   {
      public ModelManager() : base("ProductCategory")
      {

      }
      public DbSet<Category> Categories { get; set; }
      public DbSet<Product> Products { get; set; }
   }
   public class Category
   {
      [Key]
      public int Id { get; set; }
      public string CatNo { get; set; }
      public string CatName { get; set; }
   }
   public class Product
   {
      [Key]
      public int Id { get; set; }
      [Required]
      public string ProdName { get; set; }
      public string ProdCoded { get; set; }
      [DataType(DataType.Date), Column(TypeName = "date"), Display(Name = "Expire Date")]
      [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
      public DateTime ExpireDate { get; set; }
      public string Supplier { get; set; }
      public string Company { get; set; }
      [ForeignKey("Category")]
      public int CategoryId { get; set; }
      public string RackPosition { get; set; }
      public string ProductType { get; set; }
      public string AlertQuantity { get; set; }
      public string PruchasePrice { get; set; }
      public string SalesPrice { get; set; }
      public string ProductImagePath { get; set; }
      [NotMapped]
      public HttpPostedFileBase ProductImage { get; set; }
      public string Stock { get; set; }

      public Category Category { get; set; }
   }
}