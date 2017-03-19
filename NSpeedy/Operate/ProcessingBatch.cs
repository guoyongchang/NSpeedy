using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy
{
    public interface ProcessingBatch
    {
        int ExecuteUpdate(SqlConnection conn, IDictionary<string, object> dic);
    }
}
