using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Swagger.Common
{
    public abstract class TemplatePage : Controller
    {
        protected int UserId
        {
            get
            {
                int userid = Request.Query["userid"].ToInt();
                return  userid == 0 ? 33316 : userid;
            }
        }

        protected WJson Wjson = new WJson();


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Response.Headers.Clear();
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST");
            Response.Headers.Add("Access-Control-Max-Age", "2000");
        }
    }
}