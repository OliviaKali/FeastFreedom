using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeastFreedom.Models;

namespace FeastFreedom.Controllers
{
    public class MenusController : Controller
    {
        private FeastFreedomEntities db = new FeastFreedomEntities();
      

        // GET: Menus
        public ActionResult Index(int? id)
        {
            var kitchenID = id;
            id = Convert.ToInt32(Session["LoginKitchenID"]);
            var menu = db.Menus.Where(x => x.FK_KitchenID == id);
            var kitchen = db.Kitchens.Find(id);
            //var menus = db.Menus.Include(m => m.Kitchen);
            //return View(new { id = Session["LoginKitchenID"] });
            //return RedirectToAction("Index", "Menus", new { id = Session["LoginKitchenID"] });
            return View(menu.ToList());
        }

        //Menus/RestaurantMenu/id
        public ActionResult RestaurantMenu(int id) 
        {
            ViewBag.UserID = Session["userId"];
            Session["KitchenID"] = id;
            


            var menu = db.Menus.Where(x => x.FK_KitchenID == id);
            var kitchen = db.Kitchens.Find(id);

            Session["KitchenName"] = kitchen.KitchenName;
            Session["KitchenEmail"] = kitchen.KitchenEmail;


            dynamic MultiModel = new ExpandoObject();
            MultiModel.KITCHEN = kitchen;

            MultiModel.MENU = menu;

            return View(MultiModel);
        }


        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            ViewBag.data = TempData["successMsg"];
            

            ViewBag.FK_KitchenID = new SelectList(db.Kitchens, "KitchenID", "KitchenName");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuItemID,MenuItemName,Veg_NoVeg,MenuItemPrice,FK_KitchenID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (Session["KitchenID"] != null){
                    var fkKitchenID = Convert.ToInt32(Session["KitchenID"]);
                    menu.FK_KitchenID = fkKitchenID;
                }
                else if (Session["LoginKitchenID"] != null)
                {
                    var fkKitchenID = Convert.ToInt32(Session["LoginKitchenID"]);
                    menu.FK_KitchenID = fkKitchenID;
                }
            
                db.Menus.Add(menu);
                db.SaveChanges();
                //return RedirectToAction("RestaurantMenu", "Menus", new { id = Session["KitchenID"] });
                Session["MenuItemID"] = menu.MenuItemID;
                return RedirectToAction("ThankYou");
            }

            ViewBag.FK_KitchenID = new SelectList(db.Kitchens, "KitchenID", "KitchenName", menu.FK_KitchenID);
            return View(menu);
        }

        public ActionResult ThankYou()
        {
            return View();
       
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_KitchenID = Session["LoginKitchenID"];
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuItemID,MenuItemName,Veg_NoVeg,MenuItemPrice,FK_KitchenID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_KitchenID = new SelectList(db.Kitchens, "KitchenID", "KitchenName", menu.FK_KitchenID);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
