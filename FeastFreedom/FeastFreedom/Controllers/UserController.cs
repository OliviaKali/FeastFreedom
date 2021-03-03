using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeastFreedom.Models;
namespace FeastFreedom.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            User user = new User();
            return View(user);
        }


        [HttpPost]

        public ActionResult Register([Bind(Include = "UserID,FirstName,LastName,UserPassword,Email,SecurityAnswer1,SecurityAnswer2")] User user)
        {
            using (FeastFreedomEntities db = new FeastFreedomEntities())
            {
                if (db.Users.Any(x => x.Email == user.Email))
                {
                    ViewBag.DuplicateMessage = "User Already Exists";
                    return View("Register", user);
                }
                user.FK_RoleID = 2;
                db.Users.Add(user);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "You Have Been Registed!";
            TempData["UserSuccessMsg"] = "User successfully registered. Please log in to utlize Feast Freedom services!";

            return RedirectToAction("Login", "User");
            //return RedirectToAction("Index", "Kitchens", new User());
        }


        public ActionResult Login()
        {
            ViewBag.userData = TempData["UserSuccessMsg"];
            return View();
        }

        [HttpPost]

        public ActionResult Login(FeastFreedom.Models.User user)
        {
            using (FeastFreedomEntities db = new FeastFreedomEntities())
            {
                var userDetail = db.Users.Where(x => x.Email == user.Email && x.UserPassword == user.UserPassword).FirstOrDefault();
                if (userDetail == null)
                {
                    ViewBag.Error = "User does not Exist.";
                    return View("login", user);
                }
                else
                {
                    Session["userId"] = userDetail.UserID;
                    Session["UserName"] = userDetail.FirstName + " " + userDetail.LastName;
                    Session["UserEmail"] = userDetail.Email;
                    return RedirectToAction("Index", "Kitchens");
                    //return RedirectToAction("Index", "Application");
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




