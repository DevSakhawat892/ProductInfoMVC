using EmployeeInfoMVC.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EmployeeInfoMVC.Controllers
{
   public class CategoriesController : Controller
   {
      ModelManager _context;
      public ActionResult Index()
      {
         _context = new ModelManager();
         var category = _context.Categories.ToList();
         return View(category);
      }
      [HttpGet]
      public ActionResult Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Category category = _context.Categories.Find(id);
         if (category == null)
         {
            return HttpNotFound();
         }
         return View(category);
      }
      [HttpGet]
      public ActionResult Insert()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Insert(Category category)
      {
         using (_context = new ModelManager())
         {
            if (ModelState.IsValid)
            {
               _context.Categories.Add(category);
               _context.SaveChanges();
               return RedirectToAction("Index");
            }
         }
         return View(category);
      }

      [HttpGet]
      public ActionResult Update(int? id)
      {
         using (_context = new ModelManager())
         {
            if (id == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
               return HttpNotFound();
            }
            return View(category);
         }
      }


      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Update(Category category)
      {
         using (_context = new ModelManager())
         {
            if (ModelState.IsValid)
            {
               _context.Entry(category).State = EntityState.Modified;
               _context.SaveChanges();
               return RedirectToAction("Index");
            }
         }
         return View(category);
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
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
               return HttpNotFound();
            }

            return View(category);
         }
      }


      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int? id)
      {
         using (_context = new ModelManager())
         {
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
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