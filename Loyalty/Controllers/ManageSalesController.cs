using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
            return View(db.ProductLines.Include(p => p.Customer).Include(p => p.Customer.User).Include(p => p.Product).ToList().Where(p => p.Customer.User.Status == true).Where(p => p.Product.Status == true));
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
            ViewBag.CustomerId = new SelectList(db.Customers.Include(a => a.User).Where(a => a.User.Status == true).Select(a => new { a.User.Name, a.Id }).ToList(), "Id", "Name");

            ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Status == true).Select(a => new { a.Name, a.Id }).ToList(), "Id", "Name");

            return View(new ProductLine());
        }

        // POST: ManageInventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ProductLine productLine, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                Int64 CustomerId = Convert.ToInt64(data["CustomerId"]);

                Int64 ProductId = Convert.ToInt64(data["ProductId"]);

                if (CustomerId == null || CustomerId == 0 || ProductId == null || ProductId == 0)
                {
                    if (CustomerId == null || CustomerId == 0)
                    {
                        ModelState.AddModelError("", "Please select customer from drop down");
                    }

                    if (ProductId == null || ProductId == 0)
                    {
                        ModelState.AddModelError("", "Please select product from drop down");
                    }
                }
                else
                {
                    try
                    {
                        using (var dbContextTransaction = db.Database.BeginTransaction())
                        {
                            // Get product by id and decrease the quantity and increase the customer points

                            Product product = db.Products.Where(p => p.Id == ProductId).FirstOrDefault();

                            Customer customer = db.Customers.Include(c => c.User).Where(c => c.Id == CustomerId).FirstOrDefault();

                            if (customer == null || product == null)
                            {
                                // Error

                                ModelState.AddModelError("", "An unexpected error has occurred");

                                ViewBag.CustomerId = new SelectList(db.Customers.Include(a => a.User).Where(a => a.User.Status == true).Select(a => new { a.User.Name, a.Id }).ToList(), "Id", "Name");

                                ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Status == true).Select(a => new { a.Name, a.Id }).ToList(), "Id", "Name");

                                return View(productLine);
                            }

                            if (productLine.Quantity.Value < 1)
                            {
                                ModelState.AddModelError("", "Quantity must be greater then 0");

                                ViewBag.CustomerId = new SelectList(db.Customers.Include(a => a.User).Where(a => a.User.Status == true).Select(a => new { a.User.Name, a.Id }).ToList(), "Id", "Name");

                                ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Status == true).Select(a => new { a.Name, a.Id }).ToList(), "Id", "Name");

                                return View(productLine);
                            }

                            if (productLine.Quantity.Value > product.Quantity.Value)
                            {
                                // Error

                                ModelState.AddModelError("", "Available quantity for this product is " + product.Quantity.Value);

                                ViewBag.CustomerId = new SelectList(db.Customers.Include(a => a.User).Where(a => a.User.Status == true).Select(a => new { a.User.Name, a.Id }).ToList(), "Id", "Name");

                                ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Status == true).Select(a => new { a.Name, a.Id }).ToList(), "Id", "Name");

                                return View(productLine);
                            }

                            double totalPrice = (double)product.SellingPrice.Value * productLine.Quantity.Value;

                            if (totalPrice > customer.Balance.Value)
                            {
                                // Error

                                ModelState.AddModelError("", "Total price has exceed the customer balance. Customer balance is MYR " + customer.Balance.Value);

                                ViewBag.CustomerId = new SelectList(db.Customers.Include(a => a.User).Where(a => a.User.Status == true).Select(a => new { a.User.Name, a.Id }).ToList(), "Id", "Name");

                                ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Status == true).Select(a => new { a.Name, a.Id }).ToList(), "Id", "Name");

                                return View(productLine);
                            }

                            product.Quantity = product.Quantity.Value - productLine.Quantity.Value;

                            product.TotalSold = product.TotalSold.Value + productLine.Quantity.Value;

                            db.SaveChanges();

                            customer = db.Customers.Include(c => c.User).Where(c => c.Id == CustomerId).FirstOrDefault();

                            customer.Balance = customer.Balance.Value - totalPrice;

                            customer.AvailablePoints = customer.AvailablePoints.Value + (int)totalPrice;

                            customer.TotalPoints = customer.AvailablePoints.Value + customer.RedeemedPoints.Value;

                            db.SaveChanges();

                            ProductLine myProductLine = new ProductLine();

                            myProductLine.Product = db.Products.Where(p => p.Id == ProductId).FirstOrDefault();

                            myProductLine.Customer = db.Customers.Where(c => c.Id == CustomerId).Include(c => c.User).FirstOrDefault();

                            myProductLine.Date = DateTime.Now;

                            myProductLine.Status = true;

                            myProductLine.TrackingNumber = productLine.TrackingNumber;

                            myProductLine.Quantity = productLine.Quantity;

                            db.ProductLines.Add(myProductLine);

                            db.SaveChanges();

                            dbContextTransaction.Commit();
                        }

                        return RedirectToAction("Index");
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine(@"Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:");
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    catch (DbUpdateException e)
                    {
                        //Add your code to inspect the inner exception and/or
                        //e.Entries here.
                        //Or just use the debugger.
                        //Added this catch (after the comments below) to make it more obvious 
                        //how this code might help this specific problem
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "An unexpected error has occurred");
                        Console.WriteLine(ex.InnerException.ToString());
                    }
                }

            }

            ViewBag.CustomerId = new SelectList(db.Customers.Include(a => a.User).Where(a => a.User.Status == true).Select(a => new { a.User.Name, a.Id }).ToList(), "Id", "Name");

            ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Status == true).Select(a => new { a.Name, a.Id }).ToList(), "Id", "Name");

            return View(productLine);
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

        // GET: ManageSales/Details/5
        public ActionResult Edit(long? id, String progress)
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

            productLine.Progress = progress;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}