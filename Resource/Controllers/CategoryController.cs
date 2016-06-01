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
    }
}
