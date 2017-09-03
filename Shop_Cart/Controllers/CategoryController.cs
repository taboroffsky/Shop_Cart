using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Cart.Models;
using PagedList;


namespace Shop_Cart.Controllers
{
    public class CategoryController : Controller
    {
        ShopCartDB db = new ShopCartDB();
        // GET: Category
        public ActionResult Index(int? page)
        {
            int pagenumber = page ?? 1;
            int pageSize = 5;

            var catList = db.Categories.OrderBy(x => x.CategoryId).ToPagedList(pagenumber, pageSize);

            return View(catList);
        }

        public PartialViewResult CategoryPartial()
        {
            var categortList = db.Categories.OrderBy(x => x.Name).ToList();
            return PartialView(categortList);
        }

        //GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return Redirect("Index");
            }
            return View(category);
        }

        //GET: Category/Edit
        public ActionResult Edit(int? id)  //ERROR occures
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Category category = db.Categories.Find(id);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        //POST: Category/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect("Index");
            }

            return View(category);
        }

        //GET: Category/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Category category = db.Categories.Find(id);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        //POST: Category/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }




        //Disposing ????

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                base.Dispose(disposing);

            base.Dispose(disposing);
        }

        ~CategoryController()
        {
            db.SaveChanges();
            db.Dispose();
        }
    }
}