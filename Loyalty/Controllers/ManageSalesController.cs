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
    public class ManageSalesController : Controller
    {
        LoyaltyContainer db = new LoyaltyContainer();

        // GET: ManageSales
        public ActionResult Index()
        {
            return View(db.ProductLines.Include(p => p.Customer).Include(p => p.Product).ToList());
        }

        // GET: ManageSales/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLine productLine = db.ProductLines.Find(id);
            if (productLine == null)
            {
                return HttpNotFound();
            }
            return View(productLine);
        }

        // GET: ManageSales/Create
        public ActionResult Create()
        {
            // Populate User's and products' Dropdown
            ViewBag.CustomerId = new SelectList(db.Customers.Include(a => a.User).Select(a => new { a.User.Name, a.Id }), "Id", "Name");

            ViewBag.ProductId = new SelectList(db.Products.Select(a => new { a.Name, a.Id }), "Id", "Name");

            return View(new ProductLine());
        }

        // GET: ManageSales/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductLine ProductLine = db.ProductLines.Find(id);
            if (ProductLine == null || ProductLine.Status == false)
            {
                return HttpNotFound();
            }

            ProductLine.Status = false;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}