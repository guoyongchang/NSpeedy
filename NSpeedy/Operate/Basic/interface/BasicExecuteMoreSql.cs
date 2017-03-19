using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate.Basic
{
   public interface BasicExecuteMoreSql
    {
        int ExecuteUpdate(SqlConnection conn, IList<string> sqls, SqlTransaction tran);
    }
}
