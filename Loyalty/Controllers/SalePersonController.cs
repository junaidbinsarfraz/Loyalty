using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Loyalty.LoyaltyTest;

namespace Loyalty.Controllers
{
    public class SalePersonController : Controller
    {
        private LoyaltyTest.LoyaltyTest db = new LoyaltyTest.LoyaltyTest();

        // GET: SalePerson
        public ActionResult Index()
        {
            User user = (User)HttpContext.Session["LoggedInUser"];

            if (user == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(user);
        }

        // GET: ManageCustomers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = (User)HttpContext.Session["LoggedInUser"];

            if (user == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if(user.Id != id)
            {
                return HttpNotFound();
            }

            user = db.Users.Find(id);
            if (user == null || user.Status == false)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: ManageCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                User oldUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                oldUser.Name = user.Name;
                oldUser.Address = user.Address;
                oldUser.MobileNumber = user.MobileNumber;

                db.SaveChanges();

                User newUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                if (user != null)
                {
                    HttpContext.Session["LoggedInUser"] = newUser;
                }

                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}