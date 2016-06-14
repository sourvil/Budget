using Budget.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budget.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //var ravenClient = new RavenClient("https://3e7162206905467c859e71c38fb7e912:62ffdd05a25145f7886165e01b41b622@app.getsentry.com/82168");
            //try
            //{
            //    int i = Convert.ToInt32("s");
            //}
            //catch (Exception e)
            //{
            //    string s2 = ravenClient.Capture(new SharpRaven.Data.SentryEvent(e));
            //    Console.WriteLine(s2);
            //}
            //int i = Convert.ToInt32("a");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}