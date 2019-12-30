using System.Data;
using Microsoft.AspNetCore.Http;
using Swagger.Common;

namespace Swagger.Utils
{
    public class DbConfig
    {
        public const string ConnectionString =
            "Data Source=192.168.0.211;Initial Catalog=ActivityCenter_Act;User Id=test;Password=test;";

        public const string DapperConnString = "Data Source=192.168.0.211;Initial Catalog=ActivityCenter_Act;User Id=test;Password=test;";

        public static CDatabase GetDB()
        {
            return new CDatabase(ConnectionString, true);
        }

        public static void CloseDb(CDatabase db)
        {
            if (db != null)
            {
                db.close();
            }
        }
    }
}