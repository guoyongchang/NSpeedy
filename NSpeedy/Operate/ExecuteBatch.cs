using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate
{
    public interface ExecuteBatch
    {
        int ExecuteUpdate<T>(SqlConnection conn, IList<T> objs) where T : IdentityObj;
    }
}
