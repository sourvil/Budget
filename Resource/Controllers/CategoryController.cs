using Resource.Models.Data.Context;
using Resource.Models.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Resource.Controllers
{
    public class CategoryController : ApiController
    {
        private BudgetDBContext db = new BudgetDBContext();

        public List<Category> Get()
        {
            return db.Category.Include("SubCategory").Where(s => s.Status == 1).ToList();
        }

        public Category Get(int ID)
        {
            return db.Category.Find(ID);
        }

        [Route("api/category/create")]
        public void Create(Category category)
        {
            db.Category.Add(category);
            db.SaveChanges();
        }

        public void Put(Category category)
        {
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int ID) {
            Category category = db.Category.Find(ID);
            category.Status = 0;
            //db.Category.Remove(category);
            db.SaveChanges();
        }
    }
}
