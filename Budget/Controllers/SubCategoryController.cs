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
using Budget.Base;

namespace Budget.Controllers
{
    public class SubCategoryController : BaseController
    {
        private BudgetDBContext db = new BudgetDBContext();

        // GET: SubCategory
        public ActionResult Index()
        {
            var subCategory = db.SubCategory.Include(s => s.Category).Where(s=>s.Status == 1);
            return View(subCategory.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "Name",0);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                subCategory.Status = 1;
                db.SubCategory.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "Name", 0);
                ValidateRequest = false;
                return View(subCategory);
            }
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SubCategory subCategory = db.SubCategory.Find(id);
            
            ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "Name", subCategory.CategoryID);

            if (subCategory != null)
                return View(subCategory);
            else
                return HttpNotFound("No SubCategory!");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                subCategory.Status = 1;
                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");   
            }
            return View(subCategory);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SubCategory subCategory = db.SubCategory.Find(id);
            if (subCategory == null)
                return HttpNotFound();
            else
            {
                return View(subCategory);
            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            SubCategory subCategory = db.SubCategory.Find(id);
            subCategory.Status = 0;
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
