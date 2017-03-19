using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSpeedy.Query.Behavior
{
    public interface QueryMore
    {
        DataSet SelectWithDataSet(SqlConnection conn, string sql, params string[] tableName);
        string SelectWithJSON(SqlConnection conn, string sql);
        IList<T> Select<T>(SqlConnection conn, string sql) where T :BasicObj;
        IList<T> Select<T>(SqlConnection conn, string sql,object obj) where T : BasicObj;
        IList<dynamic> SelectWithDynamic<T>(SqlConnection conn, string sql) where T : BasicObj;

    }
}
