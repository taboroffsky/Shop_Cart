using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Cart.Models;
using PagedList;

namespace Shop_Cart.Controllers
{
    public class NewsController : Controller
    {
        ShopCartDB db = new ShopCartDB();

        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult NewsListPartial(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var newsList = db.News.ToList().ToPagedList(pageNumber,pageSize);

            return PartialView(newsList);
        }

        ~NewsController()
        {
            db.Dispose();
        }
    }
}