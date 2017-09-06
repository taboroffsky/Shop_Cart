using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Cart.Models;
using PagedList;

namespace Shop_Cart.Controllers
{
    public class ProductsController : Controller
    {
        ShopCartDB db = new ShopCartDB();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProductPartial(int? page, int? category)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            dynamic products;

            bool ajax = Request.IsAjaxRequest();


            if (category == null)
                products = db.Products.ToList().ToPagedList(pageNumber, pageSize);
            else
                products = db.Products.Where(p => p.CategoryId == category).ToList().ToPagedList(pageNumber, pageSize);
                return PartialView(products);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Product product = db.Products.Find(id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }


    }
}