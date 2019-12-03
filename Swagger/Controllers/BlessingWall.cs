using System;
using Microsoft.AspNetCore.Mvc;
using Swagger.Common;
using Swagger.Utils;

namespace Swagger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlessingWall : TemplateController
    {

        [HttpGet]
        public void Get(int page = 1, int pageSize = 10)
        {
            string key = "key";
            if (!CCache.Exists(key))
            {
                CCache.SetCache(key, DateTime.Now, 10);
            }

            var date = Convert.ToDateTime(CCache.GetCache(key));
            WJson.AddDataItem("date", date);
            WJson.AddDataItem("now",DateTime.Now);
        }

        [HttpPost]
        public void Post()
        {

        }
    }
}
