using FeastFreedom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.UI.WebControls;

namespace FeastFreedom.Controllers
{
    public class AddToCartController : Controller
    {
        // GET: AddToCart

        private FeastFreedomEntities db = new FeastFreedomEntities();
        public ActionResult Add(int? id, int? KitchenID)
        {
            var item = db.Menus.Find(id);

            if (Session["cart"] == null)
            {
                List<Menu> li = new List<Menu>();

              
                li.Add(item);
                Session["cart"] = li;
                ViewBag.cart = li.Count();

                Session["count"] = 1;
            }
            else
            {
                List<Menu> li = (List<Menu>)Session["cart"];
                li.Add(item);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
           // return RedirectToAction("MyOrder", "AddToCart");


            return RedirectToAction("RestaurantMenu", "Menus", new { id = KitchenID });
            //return RedirectToAction("RestaurantMenu", "Menus", new { id = "" });
            //return RedirectToAction("RestaurantMenu", "Menus", new { id = UrlParameter.Optional });
        }

        public ActionResult Remove(int? id)
        {


            List<Menu> li = (List<Menu>)Session["cart"];
            li.RemoveAll(x => x.MenuItemID == id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;

            //for (int i = li.Count - 1; i >= 0; i--)
            //{
            //    if (li[i].MenuItemID == id)
            //    {
            //        li.RemoveAt(i);
            //    }
            //}


            //var itemToRemove = li.Single(r => r.MenuItemID == id);
            //li.Remove(itemToRemove);

            //var item = li.SingleOrDefault(x => x.MenuItemID == id);
            ////var item = db.Menus.Find(id);
            //li.Remove(item);

            //li.Remove.item;
            //li.Remove(new Menu() { MenuItemID = id, MenuItemName =  }); ;


            return RedirectToAction("Myorder", "AddToCart");

        }

        public ActionResult MyOrder()
        {
           
            return View((List<Menu>)Session["cart"]);
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}