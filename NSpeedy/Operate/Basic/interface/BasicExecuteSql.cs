using System;
using System.Data.SqlClient;

namespace NSpeedy.Operate.Basic
{
    public interface BasicExecuteSql
    {
        int ExecuteUpdate(SqlConnection conn,  string sql, SqlTransaction tran);
    }
}
