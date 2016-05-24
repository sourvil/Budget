using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budget.Models.Data.Context;
using Budget.Models.Data.Models;

namespace Budget.Controllers
{
    public class SubCategoryController : Controller
    {
        private BudgetDBContext db = new BudgetDBContext();

        // GET: SubCategory
        public ActionResult Index()
        {
            var subCategory = db.SubCategory.Include(s => s.Category);
            return View(subCategory.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategory.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subCategory);
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
