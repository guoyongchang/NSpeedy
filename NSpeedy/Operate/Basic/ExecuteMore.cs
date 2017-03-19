using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace NSpeedy.Operate.Basic
{
    public class ExecuteMore : BasicExecuteMoreSql
    {
        public int ExecuteUpdate(SqlConnection conn, IList<string> sqls, SqlTransaction tran)
        {
            int result = 0;
            SqlCommand cmd = conn.CreateCommand();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            foreach (var sql in sqls)
            {
                cmd.CommandText = sql;
                result += cmd.ExecuteNonQuery();
            }
            cmd.Dispose();
            return result;
        }
    }
}
