using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSpeedy.Query.Execute
{
    public class CallableExecute : ExecuteQuery
    {
        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetResult(SqlConnection conn, string ps)
        {
            throw new NotImplementedException();
        }
    }
}
