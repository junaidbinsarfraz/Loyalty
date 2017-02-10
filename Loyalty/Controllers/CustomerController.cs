using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Loyalty.Controllers
{
    public class CustomerController : Controller
    {
        private LoyaltyContainer db = new LoyaltyContainer();

        // GET: SalePerson
        public ActionResult Index()
        {
            User user = (User)HttpContext.Session["LoggedInUser"];

            if (user == null || user.Customer == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(user);
        }

        // GET: SalePerson's Dashboard
        public ActionResult Dashboard()
        {
            User user = (User)HttpContext.Session["LoggedInUser"];

            if (user == null || user.Customer == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(db.ProductLines.Include(p => p.Product).Include(p => p.Customer).Include(p => p.Customer.User).Where(p => p.Customer.User.Id == user.Id).ToList());
        }

        // GET: ManageCustomers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = (User)HttpContext.Session["LoggedInUser"];

            if (user == null || user.Customer == null)
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

        // GET: ManageCustomers/Redeem/5
        public ActionResult Redeem(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = (User)HttpContext.Session["LoggedInUser"];

            if (user == null || user.Customer == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (user.Id != id)
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

        // POST: ManageCustomers/Redeem/5
        [HttpPost]
        public ActionResult Redeem(User user, FormCollection data)
        {
            Int64 points = Convert.ToInt64(data["pointstoberedeemed"]);

            user = db.Users.Find(user.Id);

            if (points < 20 || points % 20 != 0)
            {
                ModelState.AddModelError("", "Points should be multiplt of 20 and greater then 19");
                return View(user);
            }

            user = db.Users.Where(u => u.Id == user.Id).Include(u => u.Customer).FirstOrDefault();

            if(user.Customer.AvailablePoints < points)
            {
                ModelState.AddModelError("", "Available Points are less then the entered points");
                return View(user);
            }

            int moneyToBeAdded = (int)(points / 20);

            user.Customer.AvailablePoints = user.Customer.AvailablePoints.Value - (moneyToBeAdded * 20);

            user.Customer.RedeemedPoints = user.Customer.RedeemedPoints.Value + (moneyToBeAdded * 20);

            user.Customer.TotalPoints = user.Customer.RedeemedPoints.Value + user.Customer.AvailablePoints.Value;

            user.Customer.Balance = user.Customer.Balance.Value + moneyToBeAdded;

            db.SaveChanges();

            return View(user);
        }
    }
}