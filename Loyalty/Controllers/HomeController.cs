using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loyalty.Models;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
                User user = dataContext.Users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password && u.Status == true);

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

        // Fetch data for total category sold Pie chart
        [HttpGet]
        public ActionResult SumTotalSoldByCategory()
        {
            LoyaltyContainer dataContext = new LoyaltyContainer();

            List<PieChartModel> totalCategorySold = dataContext.Database.SqlQuery<PieChartModel>("select top 5 sum(p.TotalSold) Value, p.Category Label from [Loyalty].[dbo].Products p group by p.Category order by sum(p.TotalSold) desc").ToList();

            // Set color of each percecnage section

            if (totalCategorySold.Count > 0)
            {
                totalCategorySold[0].Color = "#13dafe";
                totalCategorySold[0].Highlight = "#13dafe";
            }
            if (totalCategorySold.Count > 1)
            {
                totalCategorySold[1].Color = "#6164c1";
                totalCategorySold[1].Highlight = "#6164c1";
            }
            if (totalCategorySold.Count > 2)
            {
                totalCategorySold[2].Color = "#99d683";
                totalCategorySold[2].Highlight = "#99d683";
            }

            if (totalCategorySold.Count > 3)
            {
                totalCategorySold[3].Color = "#ffca4a";
                totalCategorySold[3].Highlight = "#ffca4a";
            }
            if (totalCategorySold.Count > 4)
            {
                totalCategorySold[4].Color = "#4c5667";
                totalCategorySold[4].Highlight = "#4c5667";
            }

            return Content(JsonConvert.SerializeObject(totalCategorySold), "application/json");
        }

        // Fetch data for least products in hand Pie chart
        [HttpGet]
        public ActionResult LeastProductsInHand()
        {
            LoyaltyContainer dataContext = new LoyaltyContainer();

            List<PieChartModel> leastProductsInHand = dataContext.Database.SqlQuery<PieChartModel>("select top 5 sum(p.Quantity) Value, p.Name Label from [Loyalty].[dbo].Products p where p.Quantity > 0 group by p.Name order by sum(p.Quantity) asc").ToList();

            // Set color of each percecnage section

            if (leastProductsInHand.Count > 0)
            {
                leastProductsInHand[0].Color = "#13dafe";
                leastProductsInHand[0].Highlight = "#13dafe";
            }
            if (leastProductsInHand.Count > 1)
            {
                leastProductsInHand[1].Color = "#6164c1";
                leastProductsInHand[1].Highlight = "#6164c1";
            }
            if (leastProductsInHand.Count > 2)
            {
                leastProductsInHand[2].Color = "#99d683";
                leastProductsInHand[2].Highlight = "#99d683";
            }

            if (leastProductsInHand.Count > 3)
            {
                leastProductsInHand[3].Color = "#ffca4a";
                leastProductsInHand[3].Highlight = "#ffca4a";
            }
            if (leastProductsInHand.Count > 4)
            {
                leastProductsInHand[4].Color = "#4c5667";
                leastProductsInHand[4].Highlight = "#4c5667";
            }

            return Content(JsonConvert.SerializeObject(leastProductsInHand), "application/json");
        }

        // Fetch top 10 products sold
        [HttpGet]
        public ActionResult TopTenProductsSold()
        {
            LoyaltyContainer dataContext = new LoyaltyContainer();

            List<Product> products = dataContext.Database.SqlQuery<Product>("select top 10 p.* from [Loyalty].[dbo].Products p order by p.TotalSold desc").ToList();

            return Json(new
            {
                products
            }, JsonRequestBehavior.AllowGet);
        }
    }
}