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
    public class ManageInventoriesController : Controller
    {
        private LoyaltyContainer db = new LoyaltyContainer();

        // GET: ManageInventories
        public ActionResult Index()
        {
            var products = db.Products;
            return View(products.ToList().Where(u => u.Status == true));
        }

        // GET: ManageInventories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null || product.Status == false)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ManageInventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageInventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: ManageInventories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null || product.Status == false)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ManageInventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Product oldProduct = db.Products.FirstOrDefault(u => u.Id == product.Id);

                oldProduct.Name = product.Name;
                oldProduct.Code = product.Code;
                oldProduct.Description = product.Description;
                oldProduct.SellingPrice = product.SellingPrice;
                oldProduct.Quantity = product.Quantity;
                oldProduct.TotalSold = product.TotalSold;
                oldProduct.CostPrice = product.CostPrice;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ManageInventories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);
            if (product == null || product.Status == false)
            {
                return HttpNotFound();
            }

            product.Status = false;

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