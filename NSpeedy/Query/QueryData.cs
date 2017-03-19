using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSpeedy.Query.Behavior
{
    public interface QueryData
    {
        IDataReader GetResult(SqlConnection conn, string ps);
        List<string> GetTableFields(IDataReader dr);
        void Close();
    }
}
