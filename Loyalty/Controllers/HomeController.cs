using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loyalty.Models;
using System.Data.Entity;
using System.Linq;

namespace Loyalty.Controllers
{
    public class HomeController : Controller
    {
        // Show login page if user is not loggedin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Show dashboard of admin
        public ActionResult Index()
        {
            if (HttpContext.Session["LoggedInUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            else
            {
                return View();
            }

        }

        // Do login for a user and redirect to specific page w.r.t. user role
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                LoyaltyContainer dataContext = new LoyaltyContainer();
                // Check credentials
                User user = dataContext.Users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);

                if (user != null)
                {
                    HttpContext.Session["LoggedInUser"] = user;

                    // Check if admin
                    if (user.Role == "Admin")
                    {
                        HttpContext.Session["Role"] = "Admin";
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Role == "Sale Person")
                    {
                        HttpContext.Session["Role"] = "Sale Person";
                        return RedirectToAction("Index", "SalePerson");
                    }
                    else if (user.Role == "Customer")
                    {
                        HttpContext.Session["Role"] = "Customer";
                        return RedirectToAction("Index", "Customer");
                    }
                }
                // Invalid credentials
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View();
        }

        // Do logout
        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session["LoggedInUser"] = null;
            HttpContext.Session["Role"] = null;

            return RedirectToAction("Login", "Home");
        }
    }
}