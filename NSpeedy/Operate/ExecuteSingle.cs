using NSpeedy.Object.Basic;
using System.Data.SqlClient;

namespace NSpeedy.Operate
{
    public interface ExecuteSingle
    {
        int ExecuteUpdate<T>(SqlConnection conn, T t) where T : IdentityObj;
    }
}
