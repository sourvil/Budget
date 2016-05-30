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
using System.Data.Entity.Core.Objects;

namespace Budget.Controllers
{
    public class ItemController : Controller
    {
        private BudgetDBContext db = new BudgetDBContext();

        // GET: Item
        public ActionResult Index()
        {
            var item = db.Item.Where(s => s.Status == 1);
            return View(item.ToList());
        }

        // GET: Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            //ObjectResult<string> or = db.Decrypt(item.AmountEncrypted);
            //item.Amount = Convert.ToInt32(or.First().ToString());
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            //ViewBag.SubCategoryID = new SelectList(db.SubCategory, "SubCategoryID", "Name");

            ViewBag.SubCategoryID =
                        db.SubCategory.Where(p => p.Status == 1)
                        .Select(x => new SelectListItem
                        {
                            Value = x.SubCategoryID.ToString(),
                            Text = x.Name,
                        });
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,Amount,Date,Note,SubCategoryID")] Item item)
        {
            if (ModelState.IsValid)
            {
                //ObjectResult<byte[]> or = db.Encrypt(item.Amount);
                //item.AmountEncrypted =or.First();
                item.Status = 1;
                db.Item.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubCategoryID = new SelectList(db.SubCategory, "SubCategoryID", "Name", item.SubCategoryID);
            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubCategoryID = new SelectList(db.SubCategory, "SubCategoryID", "Name", item.SubCategoryID);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,Amount,Date,Note,SubCategoryID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoryID = new SelectList(db.SubCategory, "SubCategoryID", "Name", item.SubCategoryID);
            return View(item);
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Item.Find(id);
            //db.Item.Remove(item);
            item.Status = 0;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Chart(int? id)
        {
            return View();
        }

        [HttpPost, ActionName("Chart")]
        public JsonResult Chart()
        {
            var Items = db.Item.Include(x=>x.SubCategory.Category).Where(x => x.Status == 1).OrderBy(x => x.Date.Month);            

            List<string> lstMonth = new List<string>();
            List<string> lstExpences = new List<string>();
            List<string> lstIncome = new List<string>();


            foreach (var item in Items)
            {
                Item dbItem = item;
                if (dbItem.SubCategory.Category.CategoryType)
                {
                    lstExpences.Add(dbItem.Amount.ToString());
                }
                else
                {
                    lstIncome.Add(dbItem.Amount.ToString());
                }
                string strMonth = dbItem.Date.ToString("MMMM");
                strMonth = strMonth.UppercaseFirstLetter();
                if (!lstMonth.Contains(strMonth))
                    lstMonth.Add(strMonth);

            }


            var BarChart = new
            {
                Month = lstMonth,
                Expences = lstExpences,
                Income = lstIncome,
            };

            return Json(BarChart, JsonRequestBehavior.AllowGet);
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
public static class ExtensionMethods
{
    public static string UppercaseFirstLetter(this string value)
    {
        //
        // Uppercase the first letter in the string.
        //
        if (value.Length > 0)
        {
            char[] array = value.ToCharArray();
            array[0] = char.ToUpper(array[0]);
            return new string(array);
        }
        return value;
    }
}
