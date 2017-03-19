using NSpeedy.Operate.Basic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate.Execute
{
    public class ExecuteMoreSql : ExecuteBatchSql
    {
        private ExecuteMore executeMore;

        public ExecuteMoreSql()
        {
            executeMore = new ExecuteMore();
        }
        public int ExecuteUpdate(SqlConnection conn, IList<string> objs)
        {
            int result = -1;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlTransaction = conn.BeginTransaction();
                result = executeMore.ExecuteUpdate(conn, objs, sqlTransaction);
            }
            catch (Exception e)
            {
                sqlTransaction.Commit();
                throw new Exception($"类:{this.GetType().ToString()}异常,原因:{e.Message}");
            }
            return result;
        }
    }
}
