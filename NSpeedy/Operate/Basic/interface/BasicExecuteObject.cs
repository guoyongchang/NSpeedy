using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate.Basic.Interface
{
    public interface BasicExecuteObject
    {
        int ExecuteUpdateObject(SqlConnection conn, object obj, SqlTransaction tran);
    }
}
