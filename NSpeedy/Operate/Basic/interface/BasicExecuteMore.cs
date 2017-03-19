using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate.Basic
{
    public interface BasicExecuteMore
    {
        int ExecuteUpdate<T>(SqlConnection conn, IList<T> objs, SqlTransaction tran) where T : IdentityObj;
    }
}
