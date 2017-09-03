using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Cart.Models;

namespace Shop_Cart.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShopCartDB db = new ShopCartDB();

        private const string strCart = "Cart";

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            if(Session[strCart] == null)
            {
                List<Cart> list = new List<Cart>
                {
                    new Cart(db.Products.Find(id), 1)
                };

                Session[strCart] = list;
            }
            else
            {
                List<Cart> cart = Session[strCart] as List<Cart>;
                int check = CheckifExist(id);

                if (check == -1)
                    cart.Add(new Cart(db.Products.Find(id), 1));
                else
                    cart[check].Quantity++;
                
                Session[strCart] = cart;
            }

            return View("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            int check = CheckifExist(id);

            if(check == -1)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            List<Cart> list = (List<Cart>)Session[strCart];
            list.RemoveAt(check);

            return View("Index");
        }

        public ActionResult UpdateCart(FormCollection frc)
        {
            string[] quantities = frc.GetValues("quantity");

            List<Cart> list = (List<Cart>)Session[strCart];

            for (int i = 0; i < list.Count; i++)            
                list[i].Quantity = Convert.ToInt32(quantities[i]);
            
            Session[strCart] = list;

            return View("Index");
        }

        public ActionResult CheckOut(FormCollection fc)
        {
            return View("CheckOut");
        }


        public ActionResult ProcessOrder1(FormCollection fc)
        {
            List<Cart> list = (List<Cart>)Session[strCart];

            Order order = new Order()
            {
                CustomerName = fc["cusName"],
                CustomerPhone = fc["cusPhone"],
                CustomerAdress = fc["cusAddress"],
                CustomerEmail = fc["cusEmail"],
                OderDate = DateTime.Now,
                PaymentType = "Cash",
                Status = "In process"
            };

            db.Orders.Add(order);
            db.SaveChanges();

            foreach (Cart item in list)
            {
                OrderDetail od = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Price = Convert.ToInt32(item.Product.Price)
                };

                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            Session.Remove(strCart);

            return View("OrderSuccess");
        }
        public ActionResult ProcessOrder(Order o)
        {
            List<Cart> list = (List<Cart>)Session[strCart];

            

            db.Orders.Add(o);
            db.SaveChanges();

            foreach (Cart item in list)
            {
                OrderDetail od = new OrderDetail()
                {
                    OrderId = o.OrderId,
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Price = Convert.ToInt32(item.Product.Price)
                };

                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            Session.Remove(strCart);

            return View("OrderSuccess");
        }


        private int CheckifExist(int? id)
        {
            List<Cart> cart = Session[strCart] as List<Cart>;
            for (int i = 0; i < cart.Count; i++)            
                if (cart[i].Product.ProductId == id)
                    return i;
            return -1;            
        }
    }
}