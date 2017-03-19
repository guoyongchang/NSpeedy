using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Query.Behavior
{
    public interface QueryValue
    {
        T SelectValue<T>(SqlConnection conn, string sql);

        IList<T> SelectValueList<T>(SqlConnection conn, string sql);
    }
}
