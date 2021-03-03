using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeastFreedom.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;

namespace FeastFreedom.Controllers
{
    public class OrdersController : Controller
    {
        private FeastFreedomEntities db = new FeastFreedomEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Kitchen).Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.FK_KitchenID = new SelectList(db.Kitchens, "KitchenID", "KitchenName", Session["KitchenID"]);
            ViewBag.FK_UserID = new SelectList(db.Users, "UserID", "FirstName", Session["userId"]);
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,FK_UserID,FK_KitchenID,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.FK_UserID = Convert.ToInt32(Session["userId"]);
                order.FK_KitchenID = Convert.ToInt32(Session["KitchenID"]);

                order.OrderDate = DateTime.Now.Date;
                db.Orders.Add(order);
                db.SaveChanges();

                //var kitchen = db.Kitchens.Find(id);
                var kitchenEmail = Session["KitchenEmail"].ToString();
                var userEmail = Session["UserEmail"].ToString();
                var kitchenName = Session["KitchenName"];
        

                //Configuring webMail class to send emails 
                //gmail smtp server 
                WebMail.SmtpServer = "smtp.gmail.com";
                    //gmail port to send emails 
                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = false;
                    //sending emails with secure protocol 
                    WebMail.EnableSsl = true;
                    //EmailId used to send emails from application 
                    WebMail.UserName = "feastfreedom2@gmail.com";
                    WebMail.Password = ""; //password goes here

                    //Sender email address. 
                    WebMail.From = "feastfreedom2@gmail.com";

                    

                    //Send email 
                    WebMail.Send(to: userEmail, subject: "Feast Freedom Order", body: "Thank you for placing an order with " + kitchenName + ".");
                    WebMail.Send(to: kitchenEmail, subject: "Feast Freedom Order", body: "A customer has placed an order at your restaurant.");
                    ViewBag.Status = "Email Sent Successfully.";

                List<Menu> li = (List<Menu>)Session["cart"];
                li.RemoveRange(0, li.Count);
                //li.Clear();
                //Session["cart"] = li;
                Session["count"] = li.Count;
                


                return RedirectToAction("Index");
            }

            ViewBag.FK_KitchenID = new SelectList(db.Kitchens, "KitchenID", "KitchenName", order.FK_KitchenID);
            ViewBag.FK_UserID = new SelectList(db.Users, "UserID", "FirstName", order.FK_UserID);
            return View(order);
        }


        //public ActionResult ConfirmOrder()
        //{
        //    return RedirectToAction("");
        //}




        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_KitchenID = new SelectList(db.Kitchens, "KitchenID", "KitchenName", order.FK_KitchenID);
            ViewBag.FK_UserID = new SelectList(db.Users, "UserID", "FirstName", order.FK_UserID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,FK_UserID,FK_KitchenID,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_KitchenID = new SelectList(db.Kitchens, "KitchenID", "KitchenName", order.FK_KitchenID);
            ViewBag.FK_UserID = new SelectList(db.Users, "UserID", "FirstName", order.FK_UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
