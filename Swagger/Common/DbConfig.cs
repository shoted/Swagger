using System.Data;
using System.Data.SqlClient;

namespace Swagger.Common
{
    public class DbConfig
    {
        private const string _connectionString =
            "Data Source=.;Initial Catalog=cook;User Id=test;Password=test;";

        public static CDatabase GetDb()
        {
            return new CDatabase(_connectionString, true);
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