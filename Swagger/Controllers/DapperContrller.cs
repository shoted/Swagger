using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Swagger.Data.View;
using Swagger.Utils;

namespace Swagger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DapperController:TemplatePage
    {
        private readonly IDbConnection _dbConnection;

        public DapperController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public override void Get()
        {
            var data = _dbConnection.Query<TestContent>("select * from t_blessing_wall");

            WJson.AddDataItem("data", data);
            WJson.AddDataItem("state",_dbConnection.State);
            WJson.AddDataItem("connstring", _dbConnection.ConnectionString);
        }

        [HttpPost]
        public override void Post()
        {
            
        }
    }
}
