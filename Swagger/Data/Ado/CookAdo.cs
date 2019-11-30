using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Swagger.Common;
using Swagger.Data.View;

namespace Swagger.Data.Ado
{
    public class CookAdo
    {
        public static CookTermInfo p_cook_term_show(CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();


            db.fetch_procedure("p_cook_term_show");

            if (db.num_rows <= 0)
                return null;
            CookTermInfo c = new CookTermInfo();
            c.tid = (db.rows[0]["tid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["tid"].ToString());
            c.status = (db.rows[0]["status"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["status"].ToString());
            c.starttime = (db.rows[0]["starttime"] == System.DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(db.rows[0]["starttime"].ToString());
            c.settletime = (db.rows[0]["settletime"] == System.DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(db.rows[0]["settletime"].ToString());
            c.endtime = (db.rows[0]["endtime"] == System.DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(db.rows[0]["endtime"].ToString());
            c.balance = (db.rows[0]["balance"] == System.DBNull.Value) ? 0 : Convert.ToInt64(db.rows[0]["balance"].ToString());
            return c;
        }

        public static bool p_cook_order_insert(int userid, int bettype, int betcount, out string outmsg, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@bettype", bettype));
            parms.Add(new SqlParameter("@betcount", betcount));

            SqlParameter pOutmsg = new SqlParameter("@outmsg", SqlDbType.VarChar, 256);
            pOutmsg.Direction = ParameterDirection.Output;
            parms.Add(pOutmsg);
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);


            db.execute_procedure("p_cook_order_insert", parms);

            outmsg = (pOutmsg.Value == System.DBNull.Value) ? "" : Convert.ToString(pOutmsg.Value);

            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            if (retval >= 1)
                return true;

            return false;
        }
    }
}
