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
    public class SubCategoryController : ApiController
    {
        private BudgetDBContext db = new BudgetDBContext();
        public List<SubCategory> Get()
        {
            return db.SubCategory.Where(s => s.Status == 1).ToList();
            //return db.SubCategory.Include("Category").Where(s => s.Status == 1).ToList();
        }

        [Route("api/subcategory/create")]
        public void Create(SubCategory subCategory)
        {
            subCategory.Status = 1;
            db.SubCategory.Add(subCategory);
            db.SaveChanges();
        }
        public SubCategory Get(int ID)
        {
            return db.SubCategory.Find(ID);
        }
        public void Put(SubCategory subCategory)
        {
            subCategory.Status = 1;
            db.Entry(subCategory).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int ID)
        {
            SubCategory subCategory = db.SubCategory.Find(ID);
            subCategory.Status = 0;
            db.SaveChanges();
        }
    }
}
