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
    //[RoutePrefix("api/Item")]
    public class ItemController : ApiController
    {
        private BudgetDBContext db = new BudgetDBContext();

        public IEnumerable<Item> Get()
        {
            return db.Item.Where(s => s.Status == 1).AsEnumerable();
        }
    }
}
