using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Swagger.Common
{
    /// <summary>
    /// CDatabase: 数据库操作类
    /// 不允许在项目中执行直接SQL语句,需要通过调用存储过程来执行
    /// </summary>
    public class CDatabase : IDisposable
    {
        /// <summary>执行过程后返回rows值</summary>
        public DataRowCollection rows;
        /// <summary>执行过程返回执行行数</summary>
        public int num_rows;
        public int timeout;
        /// <summary>数据库链接</summary>
        public SqlConnection Connection;
        
         

        /// <summary>构造函数,打开数据库链接</summary>
        /// <param name="ConnectionString">数据库链接字符串</param>
        /// <param name="IsConn">ture:数据库链接字符串 false:webconfig中ConfigurationManager.AppSettings中的数据库名</param>
        public CDatabase(string ConnectionString, bool IsConn)
        {
            if (IsConn)
                this.open(ConnectionString);

        }

        /// <summary>构造函数,打开数据库链接</summary>
        /// <param name="db_host">数据库IP</param>
        /// <param name="db_user">用户</param>
        /// <param name="db_password">密码</param>
        /// <param name="db_name">数据库名称</param>
        public CDatabase(string db_host, string db_user, string db_password, string db_name)
        {
            this.open("Data Source=" + db_host + ";User Id=" + db_user + ";Password=" + db_password + ";Initial Catalog=" + db_name + ";Persist Security Info=no;Integrated Security=no;");
        }



        /// <summary>打开数据链接</summary>
        /// <param name="ConnectionString">链接字符串</param>
        private bool open(string ConnectionString)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                return false;
            if (this.Connection != null)
            {
                if (this.Connection.State == ConnectionState.Open)
                    this.Connection.Close();
            }
            try
            {
                this.Connection = new SqlConnection(ConnectionString);
                this.Connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                this.addError(this.Connection.Database, this.Connection.DataSource, "", "", ex.Message);
                return false;
            }
        }

        /// <summary>执行存储过程,不返回rows</summary>
        /// <param name="ProcedureName">过程名</param>
        public void execute_procedure(string ProcedureName)
        {
            if (this.Connection == null || this.Connection.State != ConnectionState.Open)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SqlCommand sqlCommand = new SqlCommand(ProcedureName, this.Connection);
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (this.timeout > 0)
                    sqlCommand.CommandTimeout = this.timeout;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", ex.Message);
                throw;
            }
            finally
            {
                sqlCommand?.Dispose();
            }
            stopwatch.Stop();
            this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", stopwatch.ElapsedMilliseconds);
        }

        /// <summary>执行存储过程,不返回rows</summary>
        /// <param name="ProcedureName">过程名</param>
        /// <param name="parameters">参数名</param>
        public void execute_procedure(string ProcedureName, SqlParameter[] parameters)
        {
            if (this.Connection == null || this.Connection.State != ConnectionState.Open)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string procparams = string.Empty;
            SqlCommand sqlCommand = new SqlCommand(ProcedureName, this.Connection);
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (this.timeout > 0)
                    sqlCommand.CommandTimeout = this.timeout;
                sqlCommand.Parameters.Clear();
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction != ParameterDirection.ReturnValue)
                            procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                        sqlCommand.Parameters.Add(parameter);
                    }
                }
                sqlCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                throw;
            }
            finally
            {
                sqlCommand?.Dispose();
            }
            stopwatch.Stop();
            if (!string.IsNullOrEmpty(procparams))
                procparams = procparams.TrimEnd(',');
            this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
        }

        /// <summary>执行存储过程,不返回rows</summary>
        /// <param name="ProcedureName">过程名</param>
        /// <param name="parameters">参数名</param>
        public void execute_procedure(string ProcedureName, List<SqlParameter> parameters)
        {
            if (this.Connection == null || this.Connection.State != ConnectionState.Open)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string procparams = string.Empty;
            SqlCommand sqlCommand = new SqlCommand(ProcedureName, this.Connection);
            try
            {
                if (this.timeout > 0)
                    sqlCommand.CommandTimeout = this.timeout;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Clear();
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction != ParameterDirection.ReturnValue)
                            procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                        sqlCommand.Parameters.Add(parameter);
                    }
                }
                sqlCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                throw;
            }
            finally
            {
                sqlCommand?.Dispose();
            }
            stopwatch.Stop();
            if (!string.IsNullOrEmpty(procparams))
                procparams = procparams.TrimEnd(',');
            this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
        }

        /// <summary>执行存储过程,不返回rows</summary>
        /// <param name="ProcedureName">过程名</param>
        /// <param name="parameters">参数名</param>
        /// <param name="errormsg"></param>
        public bool execute_procedure_stone(
          string ProcedureName,
          SqlParameter[] parameters,
          ref string errormsg)
        {
            bool flag = true;
            if (this.Connection != null && this.Connection.State == ConnectionState.Open)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string procparams = string.Empty;
                SqlCommand sqlCommand = new SqlCommand(ProcedureName, this.Connection);
                try
                {
                    if (this.timeout > 0)
                        sqlCommand.CommandTimeout = this.timeout;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            if (parameter.Direction != ParameterDirection.ReturnValue)
                                procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                            sqlCommand.Parameters.Add(parameter);
                        }
                    }
                    sqlCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                    errormsg = ex.Message;
                    flag = false;
                }
                finally
                {
                    sqlCommand?.Dispose();
                }
                stopwatch.Stop();
                if (!string.IsNullOrEmpty(procparams))
                    procparams = procparams.TrimEnd(',');
                this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
            }
            return flag;
        }

        /// <summary>执行存储过程,返回rows,执行完成之后调用db.rows接收返回值</summary>
        /// <param name="ProcedureName">过程名</param>
        public void fetch_procedure(string ProcedureName)
        {
            if (this.Connection == null || this.Connection.State != ConnectionState.Open)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.num_rows = 0;
            try
            {
                SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                if (this.timeout > 0)
                    selectCommand.CommandTimeout = this.timeout;
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.Clear();
                selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                try
                {
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count == 1)
                    {
                        this.rows = dataSet.Tables[0].Rows;
                        this.num_rows = this.rows.Count;
                    }
                    else
                    {
                        this.rows = (DataRowCollection)null;
                        this.num_rows = 0;
                    }
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", ex.Message);
                    throw;
                }
                finally
                {
                    dataSet?.Dispose();
                    sqlDataAdapter?.Dispose();
                    selectCommand?.Dispose();
                }
            }
            catch (SqlException ex)
            {
                this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", ex.Message);
                throw;
            }
            stopwatch.Stop();
            this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", stopwatch.ElapsedMilliseconds);
        }

        /// <summary>执行存储过程,返回rows,执行完成之后调用db.rows接收返回值</summary>
        /// <param name="ProcedureName">过程名</param>
        /// <param name="parameters">参数名</param>
        public void fetch_procedure(string ProcedureName, SqlParameter[] parameters)
        {
            if (this.Connection == null || this.Connection.State != ConnectionState.Open)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.num_rows = 0;
            string procparams = string.Empty;
            try
            {
                SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                if (this.timeout > 0)
                    selectCommand.CommandTimeout = this.timeout;
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.Clear();
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction != ParameterDirection.ReturnValue)
                            procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                        selectCommand.Parameters.Add(parameter);
                    }
                }
                selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                try
                {
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count == 1)
                    {
                        this.rows = dataSet.Tables[0].Rows;
                        this.num_rows = this.rows.Count;
                    }
                    else
                    {
                        this.rows = (DataRowCollection)null;
                        this.num_rows = 0;
                    }
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                    throw;
                }
                finally
                {
                    dataSet?.Dispose();
                    sqlDataAdapter?.Dispose();
                    selectCommand?.Dispose();
                }
            }
            catch (SqlException ex)
            {
                this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                throw;
            }
            stopwatch.Stop();
            if (!string.IsNullOrEmpty(procparams))
                procparams = procparams.TrimEnd(',');
            this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
        }

        /// <summary>执行存储过程,返回rows,执行完成之后调用db.rows接收返回值</summary>
        /// <param name="ProcedureName">过程名</param>
        /// <param name="parameters">参数名</param>
        public void fetch_procedure(string ProcedureName, List<SqlParameter> parameters)
        {
            if (this.Connection == null || this.Connection.State != ConnectionState.Open)
                return;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.num_rows = 0;
            string procparams = string.Empty;
            try
            {
                SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                if (this.timeout > 0)
                    selectCommand.CommandTimeout = this.timeout;
                selectCommand.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        if (parameter.Direction != ParameterDirection.ReturnValue)
                            procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                        selectCommand.Parameters.Add(parameter);
                    }
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                try
                {
                    sqlDataAdapter.Fill(dataSet);
                    if (dataSet.Tables.Count == 1)
                    {
                        this.rows = dataSet.Tables[0].Rows;
                        this.num_rows = this.rows.Count;
                    }
                    else
                        this.num_rows = 0;
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                    throw;
                }
                finally
                {
                    dataSet?.Dispose();
                    sqlDataAdapter?.Dispose();
                    selectCommand?.Dispose();
                }
            }
            catch (SqlException ex)
            {
                this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                throw;
            }
            stopwatch.Stop();
            if (!string.IsNullOrEmpty(procparams))
                procparams = procparams.TrimEnd(',');
            this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
        }

        /// <summary>得到DataTable</summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns></returns>
        public DataTable GetDataTable(string ProcedureName)
        {
            DataTable dataTable = (DataTable)null;
            if (this.Connection != null && this.Connection.State == ConnectionState.Open)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                this.num_rows = 0;
                try
                {
                    SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                    if (this.timeout > 0)
                        selectCommand.CommandTimeout = this.timeout;
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Clear();
                    selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                    DataSet dataSet = new DataSet();
                    try
                    {
                        sqlDataAdapter.Fill(dataSet);
                        if (dataSet.Tables.Count == 1)
                            dataTable = dataSet.Tables[0];
                    }
                    catch (SqlException ex)
                    {
                        this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", ex.Message);
                        throw;
                    }
                    finally
                    {
                        dataSet?.Dispose();
                        sqlDataAdapter?.Dispose();
                        selectCommand?.Dispose();
                    }
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", ex.Message);
                    throw;
                }
                stopwatch.Stop();
                this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", stopwatch.ElapsedMilliseconds);
            }
            return dataTable;
        }

        public DataTable GetDataTable(string ProcedureName, List<SqlParameter> parameters)
        {
            DataTable dataTable = (DataTable)null;
            if (this.Connection != null && this.Connection.State == ConnectionState.Open)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                this.num_rows = 0;
                string procparams = string.Empty;
                try
                {
                    SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                    if (this.timeout > 0)
                        selectCommand.CommandTimeout = this.timeout;
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            if (parameter.Direction != ParameterDirection.ReturnValue)
                                procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                            selectCommand.Parameters.Add(parameter);
                        }
                    }
                    selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                    DataSet dataSet = new DataSet();
                    try
                    {
                        sqlDataAdapter.Fill(dataSet);
                        if (dataSet.Tables.Count == 1)
                            dataTable = dataSet.Tables[0];
                    }
                    catch (SqlException ex)
                    {
                        this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                        throw;
                    }
                    finally
                    {
                        dataSet?.Dispose();
                        sqlDataAdapter?.Dispose();
                        selectCommand?.Dispose();
                    }
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                    throw;
                }
                stopwatch.Stop();
                if (!string.IsNullOrEmpty(procparams))
                    procparams = procparams.TrimEnd(',');
                this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
            }
            return dataTable;
        }

        /// <summary>得到DataTable</summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public DataTable GetDataTable(string ProcedureName, SqlParameter[] parameters)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DataTable dataTable = (DataTable)null;
            string procparams = string.Empty;
            if (this.Connection != null && this.Connection.State == ConnectionState.Open)
            {
                this.num_rows = 0;
                try
                {
                    SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                    if (this.timeout > 0)
                        selectCommand.CommandTimeout = this.timeout;
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            if (parameter.Direction != ParameterDirection.ReturnValue)
                                procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                            selectCommand.Parameters.Add(parameter);
                        }
                    }
                    selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                    DataSet dataSet = new DataSet();
                    try
                    {
                        sqlDataAdapter.Fill(dataSet);
                        if (dataSet.Tables.Count == 1)
                            dataTable = dataSet.Tables[0];
                    }
                    catch (SqlException ex)
                    {
                        this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                        throw;
                    }
                    finally
                    {
                        dataSet?.Dispose();
                        sqlDataAdapter?.Dispose();
                        selectCommand?.Dispose();
                    }
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                    throw;
                }
                stopwatch.Stop();
                if (!string.IsNullOrEmpty(procparams))
                    procparams = procparams.TrimEnd(',');
                this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
            }
            return dataTable;
        }

        /// <summary>得到DataSet</summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <returns></returns>
        public DataSet GetDataSet(string ProcedureName)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DataSet dataSet = new DataSet();
            if (this.Connection != null)
            {
                if (this.Connection.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                        if (this.timeout > 0)
                            selectCommand.CommandTimeout = this.timeout;
                        selectCommand.CommandType = CommandType.StoredProcedure;
                        selectCommand.Parameters.Clear();
                        selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                        try
                        {
                            sqlDataAdapter.Fill(dataSet);
                        }
                        catch (SqlException ex)
                        {
                            this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", ex.Message);
                            throw;
                        }
                        finally
                        {
                            sqlDataAdapter?.Dispose();
                            selectCommand?.Dispose();
                        }
                    }
                    catch (SqlException ex)
                    {
                        this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", ex.Message);
                        throw;
                    }
                    stopwatch.Stop();
                    this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, "", stopwatch.ElapsedMilliseconds);
                }
            }
            return dataSet;
        }

        /// <summary>得到DataSet</summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public DataSet GetDataSet(string ProcedureName, SqlParameter[] parameters)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DataSet dataSet = new DataSet();
            string procparams = string.Empty;
            if (this.Connection != null)
            {
                if (this.Connection.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                        if (this.timeout > 0)
                            selectCommand.CommandTimeout = this.timeout;
                        selectCommand.CommandType = CommandType.StoredProcedure;
                        selectCommand.Parameters.Clear();
                        if (parameters != null)
                        {
                            foreach (SqlParameter parameter in parameters)
                            {
                                if (parameter.Direction != ParameterDirection.ReturnValue)
                                    procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                                selectCommand.Parameters.Add(parameter);
                            }
                        }
                        selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                        try
                        {
                            sqlDataAdapter.Fill(dataSet);
                        }
                        catch (SqlException ex)
                        {
                            this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                            throw;
                        }
                        finally
                        {
                            sqlDataAdapter?.Dispose();
                            selectCommand?.Dispose();
                        }
                    }
                    catch (SqlException ex)
                    {
                        this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                        throw;
                    }
                    stopwatch.Stop();
                    if (!string.IsNullOrEmpty(procparams))
                        procparams = procparams.TrimEnd(',');
                    this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
                }
            }
            return dataSet;
        }

        /// <summary>得到DataSet</summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public DataSet GetDataSet(string ProcedureName, List<SqlParameter> parameters)
        {
            DataSet dataSet = new DataSet();
            if (this.Connection != null && this.Connection.State == ConnectionState.Open)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string procparams = string.Empty;
                try
                {
                    SqlCommand selectCommand = new SqlCommand(ProcedureName, this.Connection);
                    if (this.timeout > 0)
                        selectCommand.CommandTimeout = this.timeout;
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            if (parameter.Direction != ParameterDirection.ReturnValue)
                                procparams = procparams + parameter.ParameterName + "='" + parameter.Value + "',";
                            selectCommand.Parameters.Add(parameter);
                        }
                    }
                    selectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                    try
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                    catch (SqlException ex)
                    {
                        this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                        throw;
                    }
                    finally
                    {
                        sqlDataAdapter?.Dispose();
                        selectCommand?.Dispose();
                    }
                }
                catch (SqlException ex)
                {
                    this.addError(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, ex.Message);
                    throw;
                }
                stopwatch.Stop();
                if (!string.IsNullOrEmpty(procparams))
                    procparams = procparams.TrimEnd(',');
                this.AddProcTime(this.Connection.Database, this.Connection.DataSource, ProcedureName, procparams, stopwatch.ElapsedMilliseconds);
            }
            return dataSet;
        }

        /// <summary>关闭数据库链接</summary>
        public void close()
        {
            if (this.Connection == null || this.Connection.State != ConnectionState.Open)
                return;
            string database = this.Connection.Database;
            try
            {
                this.Connection.Close();
                this.Connection.Dispose();
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            this.close();
        }

        private void addError(string dbname, string dbsource, string procname, string procparams, string remark)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbname">数据库名</param>
        /// <param name="dbsource">数据库服务器IP地址</param>
        /// <param name="procname">存储过程名</param>
        /// <param name="procparams">参数</param>
        /// <param name="timetaken">耗时</param>
        /// <returns></returns>
        private bool AddProcTime(string dbname, string dbsource, string procname, string procparams, long timetaken)
        {
            return false;
        }
    }
}
