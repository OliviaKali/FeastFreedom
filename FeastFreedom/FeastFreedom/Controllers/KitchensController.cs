using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using FeastFreedom.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace FeastFreedom.Controllers
{
    public class KitchensController : Controller
    {
        private FeastFreedomEntities db = new FeastFreedomEntities();

        public string KitchenID { get; private set; }

        // GET: Kitchens
        public ActionResult Index(int? id)
        {
           // Kitchen kitchen = db.Kitchens.Find(id);

            var kitchens = db.Kitchens.Include(k => k.Role);
            ViewBag.UserID = Session["UserId"];
            //Session["KitchenID"] = kitchen.KitchenID;
            //Session["KitchenName"] = kitchen.KitchenName;

            return View(kitchens.ToList());            
        }

        public ActionResult RestaurantMenu()
        {

            return RedirectToAction("RestaurantMenu", "Menus", new { id = Session["KitchenID"] });
        }

        // GET: Kitchens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // GET: Kitchens/Create
        public ActionResult Create()
        {
            ViewBag.FK_RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View();
        }

        // POST: Kitchens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitchenID,KitchenName,KitchenEmail,KitchenPasswod,DaysWorking,HoursWorking,Image,FK_RoleID, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday")] Kitchen kitchen, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //SqlCommand cmd = new SqlCommand("INSERT INTO DaysWorking VALUES ('" + kitchen.DaysWorking + "')");
                if (kitchen.Monday == true)
                {
                    kitchen.DaysWorking = kitchen.DaysWorking + "M, ";
                }
                if (kitchen.Tuesday== true)
                {
                    kitchen.DaysWorking = kitchen.DaysWorking + "T, ";
                }
                    
                if (kitchen.Wednesday == true)
                {
                    kitchen.DaysWorking = kitchen.DaysWorking + "W, ";
                }
                   
                if (kitchen.Thursday == true)
                {
                    kitchen.DaysWorking = kitchen.DaysWorking + "Th, ";
                }
                  
                if (kitchen.Friday == true)
                {
                    kitchen.DaysWorking = kitchen.DaysWorking + "F, ";
                }
               
                if (kitchen.Saturday == true)
                {
                    kitchen.DaysWorking = kitchen.DaysWorking + "Sa, ";
                }
                   
                if (kitchen.Sunday == true)
                {
                    kitchen.DaysWorking = kitchen.DaysWorking + "Su, ";
                }
                  

                kitchen.DaysWorking = kitchen.DaysWorking.Remove(kitchen.DaysWorking.Length - 2);

                if (file != null)
                {
                    kitchen.Image = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Images/" + kitchen.Image);

                    file.SaveAs(physicalPath);
                }

                kitchen.FK_RoleID = 3;

                db.Kitchens.Add(kitchen);
                db.SaveChanges();
             
                // command.Text = "UPDATE Kitchen SET FK_RoleID = 3;";
                //Context.Kitchens.Where(x => x.FK_RoleID == 3).UpdateFromQuery(x => new Kitchen());

                TempData["successMsg"] = "Restaurant successfully registered.";
                Session["KitchenID"] = kitchen.KitchenID;
                Session["KitchenName"] = kitchen.KitchenName;

                return RedirectToAction("Create", "Menus", new { area = "" });
            }
            //ViewBag.FK_RoleID = new SelectList(db.Roles, "RoleID", "RoleName", kitchen.FK_RoleID);
            return View(kitchen);    
        }

    // GET: Kitchens/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_RoleID = new SelectList(db.Roles, "RoleID", "RoleName", kitchen.FK_RoleID);
            return View(kitchen);
        }

        // POST: Kitchens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitchenID,KitchenName,KitchenEmail,KitchenPasswod,DaysWorking,HoursWorking,Image,FK_RoleID")] Kitchen kitchen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitchen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_RoleID = new SelectList(db.Roles, "RoleID", "RoleName", kitchen.FK_RoleID);
            return View(kitchen);
        }

        // GET: Kitchens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // POST: Kitchens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitchen kitchen = db.Kitchens.Find(id);
            db.Kitchens.Remove(kitchen);
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


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FeastFreedom.Models.Kitchen kitchen)
        {
            using (FeastFreedomEntities db = new FeastFreedomEntities())
            {
                var kitchenDetail = db.Kitchens.Where(x => x.KitchenName == kitchen.KitchenName && x.KitchenPasswod == kitchen.KitchenPasswod).FirstOrDefault();
                if (kitchen == null)
                {
                    return View("login", kitchen);
                }
                else
                {
                    Session["userId"] = kitchenDetail.KitchenID;
                    Session["UserName"] = kitchenDetail.KitchenName;
                    Session["LoginKitchenID"] = kitchenDetail.KitchenID;
                    Session["KitchenName"] = kitchenDetail.KitchenName;
                    return RedirectToAction("Create", "Menus", new { area = "" });
                    //return RedirectToAction("Index", "Kitchens");

                }
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
