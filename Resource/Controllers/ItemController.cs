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

        public List<Item> Get()
        {
            return db.Item.Include("SubCategory").Include("SubCategory.Category").Where(s => s.Status == 1).OrderBy(x => x.Date.Month).ToList();
        }

        public Item Get(int ID)
        {
            return db.Item.Find(ID);
        }

        
        [Route("api/item/create")]
        public void Create(Item item)
        {
            item.Status = 1;
            db.Item.Add(item);
            db.SaveChanges();
        }

        public void Put(Item item)
        {
            item.Status = 1;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int ID)
        {
            Item item = db.Item.Find(ID);
            item.Status = 0;
            db.SaveChanges();
        }

        [HttpGet]
        [Route("api/item/chart")]
        public BarChart Chart()
        {
            List<Item> Items = Get();

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
                //strMonth = strMonth.UppercaseFirstLetter();
                if (!lstMonth.Contains(strMonth))
                    lstMonth.Add(strMonth);

            }

            BarChart bc = new BarChart();
            bc.lstExpences = lstExpences;
            bc.lstIncome = lstIncome;
            bc.lstMonth = lstMonth;

            //var BarChart = new
            //{
            //    Month = lstMonth,
            //    Expences = lstExpences,
            //    Income = lstIncome,
            //};

            return bc;
            //return Json(BarChart, JsonRequestBehavior.AllowGet);
            //return BarChart;
        }

    }
}
