using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate
{
    interface ExecuteSingleSql
    {
        int ExecuteUpdate(SqlConnection conn, string sql);
    }
}
