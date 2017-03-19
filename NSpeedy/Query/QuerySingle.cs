using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Query.Behavior
{
    public interface QuerySingle
    {
        T SelectRelyOnKey<T>(SqlConnection conn, string id) where T : BasicObj;
        T SelectOne<T>(SqlConnection conn, string sql) where T : BasicObj;
        T SelectOne<T>(SqlConnection conn, T t) where T : IdentityObj;
        IDictionary<string, object> SelectOneData(SqlConnection conn, string sql);
    }
}
