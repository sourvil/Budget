using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects;
using Budget.Base;
using Resource.Models.Data.Models;
using System.Net.Http;

namespace Budget.Controllers
{
    public class ItemController : BaseController
    {

        // GET: Item
        public ActionResult Index()
        {
            var result = GetWebApiResult("api/item", new List<Item>());
            if (result.Count > 0)
                return View(result as List<Item>);
            else
                return View();

        }

        // GET: Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = GetWebApiResult("api/item/" + id, new Item());
            if (result == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(result as Item);
            }
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            GetSubCategory();
            return View();
        }

        private void GetSubCategory(int? SubCategoryID = 0)
        {
            var result = GetWebApiResult("api/subcategory", new List<SubCategory>());
            ViewBag.SubCategoryID = new SelectList(result as List<SubCategory>, "SubCategoryID", "Name", SubCategoryID);
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
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.PostAsJsonAsync<Item>("api/item/create", item).Result;
                return RedirectToAction("Index");
            }
            else
            {
                GetSubCategory(item.SubCategoryID);
                return View(item);
            }            
          }

        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = GetWebApiResult("api/item/" + id, new Item());
            if (result == null)
            {
                return HttpNotFound();
            }
            else
            {
                GetSubCategory(((Item)result).SubCategoryID);
                return View((Item)result);
            }           
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
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.PutAsJsonAsync<Item>("api/item", item).Result;

                return RedirectToAction("Index");
            }
            else
            {
                GetSubCategory(item.SubCategoryID);
                return View(item);
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = GetWebApiResult("api/item/" + id, new Item());
            if (result == null)
            {
                return HttpNotFound();
            }
            else
            {
                GetSubCategory(((Item)result).SubCategoryID);
                return View((Item)result);
            }
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.DeleteAsync("api/item/" + id).Result;

            }
            return RedirectToAction("Index");
        }

        public ActionResult Chart(int? id)
        {
            return View();
        }

        [HttpGet, ActionName("Chart")]
        public JsonResult Chart()
        {
            var Items = GetWebApiResult("api/item", new List<Item>());

            List<string> lstMonth = new List<string>();
            List<string> lstExpences = new List<string>();
            List<string> lstIncome = new List<string>();


            foreach (var item in Items)
            {
                if (item.SubCategory.Category.CategoryType)
                {
                    lstExpences.Add(item.Amount.ToString());
                }
                else
                {
                    lstIncome.Add(item.Amount.ToString());
                }
                string strMonth = item.Date.ToString("MMMM");
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
