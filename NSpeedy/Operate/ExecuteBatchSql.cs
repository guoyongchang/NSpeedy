using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate
{
    public interface ExecuteBatchSql
    {
        int ExecuteUpdate(SqlConnection conn, IList<string> objs);
    }
}
