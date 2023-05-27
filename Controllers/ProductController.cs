using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindTutorialMVC.Models;
using PagedList;
using PagedList.Mvc;

namespace NorthwindTutorialMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var result = db.Products.ToList();
            var result = db.Products.ToList().ToPagedList(sayfa,20);
            return View(result);
        }

        [HttpGet]
        public ActionResult New()
        {
            List<SelectListItem> CategoryList = (
                from i in db.Categories.ToList()
                select new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryID.ToString()
                }).ToList();
            ViewBag.CtgList = CategoryList;
            return View();
        }

        [HttpPost]
        public ActionResult New(Products p)
        {
            var ctg = db.Categories.Where(m => m.CategoryID == p.Categories.CategoryID).FirstOrDefault();
            p.Categories = ctg;
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var result = db.Products.Find(id);
            List<SelectListItem> CategoryList = (
                from i in db.Categories.ToList()
                select new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryID.ToString()
                }).ToList();
            ViewBag.CtgList = CategoryList;
            return View("Update", result);
        }

        [HttpPost]
        public ActionResult Update(Products p)
        {
            var ctg = db.Categories.Where(m => m.CategoryID == p.Categories.CategoryID).FirstOrDefault();
            var result = db.Products.Find(p.ProductID);
            result.ProductName = p.ProductName;
            result.CategoryID = ctg.CategoryID;
            result.UnitPrice = p.UnitPrice;
            result.UnitsInStock = p.UnitsInStock;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var result = db.Products.Find(id);
            db.Products.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}