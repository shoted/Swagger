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
    [Route("[controller]")]
    public class BlessingWall : TemplatePage
    {
        
        /// <summary>
        /// 获取祝福墙
        /// </summary>
        [HttpGet]
        protected  void Get()
        {
            
        }

        /// <summary>
        /// 发送祝福
        /// </summary>
        [HttpPost]
        protected  void Post()
        {
            
        }
    }
}
