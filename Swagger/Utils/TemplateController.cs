using System.Buffers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swagger.Common;

namespace Swagger.Utils
{
    public abstract class TemplatePage : Controller
    {
        protected int UserId
        {
            get
            {
                int userid = CTools.ToInt(Request.Query["userid"]);
                return userid == 0 ? 33316 : userid;
            }
        }

        protected WJson WJson = new WJson();

        public abstract void Get();
        public abstract void Post();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Response.Headers.Clear();
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST");
            Response.Headers.Add("Access-Control-Max-Age", "2000");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                // 设置为驼峰命名
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string response = JsonConvert.SerializeObject(WJson, serializerSettings);
            //var buffer = Encoding.GetEncoding("gb2312").GetBytes(response);
            var buffer = Encoding.UTF8.GetBytes(response);
            Response.BodyWriter.Write(buffer);
        }
    }
}