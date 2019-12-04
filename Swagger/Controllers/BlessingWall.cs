using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swagger.Common;
using Swagger.Data.Ado;
using Swagger.Data.View;
using Swagger.Utils;

namespace Swagger.Controllers
{
    [ApiController]
    [Route("BlessingWall")]
    public class BlessingWall : TemplatePage
    {

        /// <summary>
        /// 获取祝福墙
        /// </summary>
        [HttpGet]
        public void Get()
        {
            string key = "key";
            CCache.SetCache(key, DateTime.Now, 10);
            WJson.AddDataItem("now", CCache.GetCache(key));
        }

        /// <summary>
        /// 发送祝福
        /// </summary>
        [HttpPost]
        public void Post()
        {

        }
    }
}
