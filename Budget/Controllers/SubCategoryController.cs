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
    public class SubCategoryController : BaseController
    {      
                // GET: SubCategory
        public ActionResult Index()
        {
            var result = GetWebApiResult("api/subcategory", new List<Resource.Models.Data.Models.SubCategory>());
            if (result.Count > 0)
                return View(result as List<Resource.Models.Data.Models.SubCategory>);
            else
                return View();
        }

        public ActionResult Create()
        {
            GetCategoryList();
            return View();
        }

        private void GetCategoryList(int? CategoryID = 0)
        {
            var result = GetWebApiResult("api/category", new List<Resource.Models.Data.Models.Category>());
            ViewBag.CategoryID = new SelectList(result as List<Resource.Models.Data.Models.Category>, "CategoryID", "Name", CategoryID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Resource.Models.Data.Models.SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.PostAsJsonAsync<Resource.Models.Data.Models.SubCategory>("api/subcategory/create", subCategory).Result;
                return RedirectToAction("Index");
            }
            else
            {
                GetCategoryList();
                ValidateRequest = false;
                return View(subCategory);
            }
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = GetWebApiResult("api/subcategory/" + id, new Resource.Models.Data.Models.SubCategory());
            if (result != null)
            {
                GetCategoryList((result as Resource.Models.Data.Models.SubCategory).CategoryID);
                return View(result as Resource.Models.Data.Models.SubCategory);
            }
            else
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Resource.Models.Data.Models.SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.PutAsJsonAsync<Resource.Models.Data.Models.SubCategory>("api/subcategory", subCategory).Result;

                return RedirectToAction("Index");
            }
            return View(subCategory);            
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var result = GetWebApiResult("api/subcategory/" + id, new Resource.Models.Data.Models.SubCategory());

            if (result == null)
                return HttpNotFound();
            else
            {
                return View(result as Resource.Models.Data.Models.SubCategory);
            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = GetHttpClient();

                HttpResponseMessage Response = null;

                Response = hc.DeleteAsync("api/subcategory/" + id).Result;

            }
            return RedirectToAction("Index");
        }
    }
}
