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
    public class ManageCustomersController : Controller
    {
        private LoyaltyContainer db = new LoyaltyContainer();

        // GET: ManageCustomers
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Customer);
            return View(users.ToList().Where(u => u.Customer != null).Where(u => u.Status == true));
        }

        // GET: ManageCustomers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null || user.Status == false)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: ManageCustomers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                User oldUser = db.Users.Where(u => u.Status == true).Where(u => u.Username == user.Username).FirstOrDefault();

                if (oldUser != null)
                {
                    ModelState.AddModelError("", "User already exists");
                    return View(user);
                }

                // add role as doctor
                user.Role = "Customer";

                // Generate MemberId
                Int64 numberOfCustmers = db.Customers.ToList().LongCount();

                if (numberOfCustmers == 0)
                {
                    user.Customer.MemberId = "170001";
                }
                else
                {
                    Customer lastCustomer = db.Customers.ToList().LastOrDefault();
                    Int64 memberId = Int64.Parse(lastCustomer.MemberId);

                    memberId++;

                    user.Customer.MemberId = memberId + "";
                }

                db.Users.Add(user);

                db.SaveChanges();

                //User Admin = (User)HttpContext.Session["LoggedInUser"];

                //if (Admin != null && Admin.Role.Name == "Admin")
                //{
                //    HttpContext.Session["TotalPatientList"] = db.Users.Include(u => u.Patient).Where(u => u.Patient != null).Where(u => u.Patient.Status == "Admitted").ToList();
                //    HttpContext.Session["TotalPatients"] = db.Users.Count(u => u.Patient != null && u.Patient.Status == "Admitted");
                //    HttpContext.Session["TotalCaregiverList"] = db.Users.Include(u => u.Caregiver).Where(u => u.Caregiver != null).ToList();
                //    HttpContext.Session["TotalCareGivers"] = db.Users.Count(u => u.Caregiver != null);
                //    HttpContext.Session["TotalDoctorList"] = db.Users.Include(u => u.Doctor).Where(u => u.Doctor != null).ToList();
                //    HttpContext.Session["TotalDoctors"] = db.Users.Count(u => u.Doctor != null);
                //}

                return RedirectToAction("Index");
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
            User user = db.Users.Find(id);
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
                User oldUser = db.Users.Include(u => u.Customer).FirstOrDefault(u => u.Id == user.Id);

                oldUser.Name = user.Name;
                oldUser.Address = user.Address;
                oldUser.MobileNumber = user.MobileNumber;
                oldUser.Customer.Balance = user.Customer.Balance;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: ManageCustomers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null || user.Status == false)
            {
                return HttpNotFound();
            }

            user.Status = false;

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
        
    }
}