using NSpeedy.Object.Basic;
using System.Data.SqlClient;

namespace NSpeedy.Operate.Basic
{
    public interface BasicExecute
    {
        int ExecuteUpdate<T>(SqlConnection conn,T obj, SqlTransaction tran) where T : IdentityObj;
    }
}
