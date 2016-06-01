using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Budget.Base
{
    public class BaseController : Controller
    {
        protected T GetWebApiResult<T>(string apiPath, T t)
        {
            HttpClient hc = new HttpClient();
            string uri = Common.ConfigValue.resourceServerUrl;
            hc.BaseAddress = new Uri(uri);

            var Result = hc.GetAsync(apiPath).Result;

            if (Result.IsSuccessStatusCode)
            {
                return Result.Content.ReadAsAsync<T>().Result;
            }
            return t;
        }
    }
}