using Swagger.Common;

namespace Swagger.Utils
{
    public class DbConfig
    {
        private const string _connectionString =
            "Data Source=192.168.0.211;Initial Catalog=ActivityCenter_Act;User Id=test;Password=test;";

        public static CDatabase GetDB()
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