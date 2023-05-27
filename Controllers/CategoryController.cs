using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindTutorialMVC.Models;

namespace NorthwindTutorialMVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Index()
        {
            var result = db.Categories.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View("New");
        }

        [HttpPost]
        public ActionResult New(Categories c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var result = db.Categories.Find(id);
            return View("Update", result);
        }

        [HttpPost]
        public ActionResult Update(Categories c)
        {
            var result = db.Categories.Find(c.CategoryID);
            result.CategoryName = c.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var result = db.Categories.Find(id);
            db.Categories.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}