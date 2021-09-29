﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechReviewPoint.Models;
using System.IO;

namespace TechReviewPoint.Controllers
{
    public class ProductDetailsController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        loginUser abc = new loginUser();

        // GET: Products

        public ActionResult ProductDashboard()
        {

            var products = db.Products.Include(q => q.Category);
            return View(products.ToList());

        }

        // GET: ProductDetails
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product pro = db.Products.Find(id);
            Session["product_ID"] = pro.ProductID;

            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpGet]
        public ActionResult search()
        {
            using (db)
            {
               // var ads = db.Products.SqlQuery("Select *from Products where PriorityStatus>-1")

                var ads = db.Products.SqlQuery("Select *from Products")
                      .ToList<Product>();

                //var ads = db.Products.SqlQuery("Select Categories.CategoryName,Products.ProductName,Products.ProductPrice from Products inner join Categories on Products.CategoryID=Categories.CategoryID").ToList<Product>();
                
               

                ViewData["Ads"] = ads;
                return View();
            }
           // return View();
        }

        [HttpPost]
        public ActionResult search(TempSearch ta)
        {
            if (ModelState.IsValid)
            {

               // var ads = db.Products.SqlQuery("Select Categories.CategoryName,Products.ProductName,Products.ProductPrice from Products inner join Categories on Products.CategoryID=Categories.CategoryID where CategoryName like '%" + (ta.CategoryName ?? "%") + "%'").ToList<Product>();


               // string t = "Select Categories.CategoryName,Products.ProductName,Products.ProductPrice from Products inner join Categories on Products.CategoryID=Categories.CategoryID where CategoryName like '%" + (ta.CategoryName ?? "%") + "%'";


                var ads = db.Products.SqlQuery("Select *from Products where ProductName like '%" + (ta.ProductName ?? "%") + "%'")
                   .ToList<Product>();

                 string t = "Select *from Products where ProductName like '%" + (ta.ProductName ?? "%") + "%'";
                System.Diagnostics.Debug.WriteLine(t);
                if (ads == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Ads"] = ads;
                    return View();
                }

            }

            return View();

        }


        }

}