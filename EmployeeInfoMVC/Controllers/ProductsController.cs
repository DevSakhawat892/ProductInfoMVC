using EmployeeInfoMVC.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EmployeeInfoMVC.Controllers
{
   public class ProductsController : Controller
   {
      // GET: Products
      ModelManager _context;
      public ActionResult Index()
      {
         _context = new ModelManager();
         var productList = _context.Products.Include(p => p.Category);
         return View(productList.ToList());
      }
      [HttpGet]
      public ActionResult Details(int? id)
      {
         using (_context = new ModelManager())
         {
            if (id == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
               return HttpNotFound();
            }
            return View(product);
         }

      }
      [HttpGet]
      public ActionResult Insert()
      {
         _context = new ModelManager();
         ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CatName");
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Insert(Product product)
      {

         using (_context = new ModelManager())
         {

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CatName", product.CategoryId);
            if (ModelState.IsValid)
            {
               if (product.ProductImage != null)
               {
                  string fPath = Path.Combine(Server.MapPath("~/"), "Images/Products");
                  if (!Directory.Exists(fPath))
                  {
                     Directory.CreateDirectory(fPath);
                  }
                  string fExt = Path.GetExtension(product.ProductImage.FileName);
                  if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
                  {
                     string fNameWithoutSpace = product.ProdName.Replace(" ", "_");
                     string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
                     "") + fExt;
                     string fileToSave = Path.Combine(fPath, fName);
                     product.ProductImage.SaveAs(fileToSave);
                     product.ProductImagePath = "~/Images/Products/" + fName;
                  }
               }
               _context.Products.Add(product);
               _context.SaveChanges();
               return RedirectToAction("Index");
            }
            return View(product);
         }
      }

      [HttpGet]
      public ActionResult Update(int? id)
      {
         _context = new ModelManager();
         ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CatName");

         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Product product = _context.Products.Find(id);
         if (product == null)
         {
            return HttpNotFound();
         }
         return View(product);
      }


      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Update(Product product)
      {
         using (_context = new ModelManager())
         {

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CatName", product.CategoryId);
            if (product.ProductImage != null)
            {
               string fPath = Path.Combine(Server.MapPath("~/"), "Images/Products");
               if (!Directory.Exists(fPath))
               {
                  Directory.CreateDirectory(fPath);
               }
               string fExt = Path.GetExtension(product.ProductImage.FileName);
               if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
               {
                  string fNameWithoutSpace = product.ProdName.Replace(" ", "_");
                  string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
                  "") + fExt;
                  string fileToSave = Path.Combine(fPath, fName);
                  product.ProductImage.SaveAs(fileToSave);
                  product.ProductImagePath = "~/Images/Products/" + fName;
               }
            }
            if (ModelState.IsValid)
            {
               _context.Entry(product).State = EntityState.Modified;
               _context.SaveChanges();
               return RedirectToAction("Index");
            }
         }
         return View(product);
      }

      [HttpGet]
      public ActionResult Delete(int? id)
      {
         using (_context = new ModelManager())
         {
            if (id == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
               return HttpNotFound();
            }
            return View(product);
         }

      }


      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int? id)
      {
         using (_context = new ModelManager())
         {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
         }
      }

      //protected override void Dispose(bool disposing)
      //{
      //   if (disposing)
      //   {
      //      _context.Dispose();
      //   }
      //   base.Dispose(disposing);
      //}
   }
}