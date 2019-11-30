using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swagger.Common;
using Swagger.Data.Ado;
using Swagger.Data.View;

namespace Swagger.Controllers
{
    [ApiController]
    [Route("cook")]
    public class CookController : TemplatePage
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            CookTermInfo termInfo = null;

            using (var db = DbConfig.GetDb())
            {
                termInfo = CookAdo.p_cook_term_show(db);
            }

            Wjson.AddDataItem("termInfo", termInfo);
            Wjson.SetValues(true, HttpStatusCode.OK, "请求成功");
            return Ok(Wjson);
        }

        /// <summary>
        /// 下注
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(int betType, long betStone)
        {
            var result = false;
            string outmsg = "";
            using (var db = DbConfig.GetDb())
            {
                result = CookAdo.p_cook_order_insert(UserId, betType, 1, out outmsg, db);
            }
            Wjson.SetValues(result, HttpStatusCode.Created, outmsg);
            return Ok(Wjson);
        }
    }
}
