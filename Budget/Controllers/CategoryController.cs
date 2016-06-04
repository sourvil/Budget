using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budget.Base;
using System.Net.Http;

namespace Budget.Controllers
{
    public class CategoryController : BaseController
    {
        //private BudgetDBContext db = new BudgetDBContext();

        // GET: Category
        public ActionResult Index()
        {
            var result = GetWebApiResult("api/Category", new List<Resource.Models.Data.Models.Category>());
            if (result.Count > 0)
                return View(result as List<Resource.Models.Data.Models.Category>);
            else
                return View();
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = GetWebApiResult("api/Category/" + id, new Resource.Models.Data.Models.Category());
            if (result != null)
                return View(result as Resource.Models.Data.Models.Category);
            else
                return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,CategoryType,Status")] Resource.Models.Data.Models.Category category)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.PostAsJsonAsync<Resource.Models.Data.Models.Category>("api/category/create", category).Result;
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = GetWebApiResult("api/Category/" + id, new Resource.Models.Data.Models.Category());
            if (result != null)
                return View(result as Resource.Models.Data.Models.Category);
            else
                return View();
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name,CategoryType,Status")] Resource.Models.Data.Models.Category category)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.PutAsJsonAsync<Resource.Models.Data.Models.Category>("api/category", category).Result;

                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = GetWebApiResult("api/Category/" + id, new Resource.Models.Data.Models.Category());
            if (result != null)
                return View(result as Resource.Models.Data.Models.Category);
            else
                return View();
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.DeleteAsync("api/category/" + id).Result;

            }
            return RedirectToAction("Index");
        }
    }
}
