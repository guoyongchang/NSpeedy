using NSpeedy.Operate.Basic.Interface;
using System.Data;
using System.Data.SqlClient;
using System;

namespace NSpeedy.Operate.Basic
{
    public class Execute : BasicExecuteSql, BasicExecuteObject
    {
        public int ExecuteUpdate(SqlConnection conn, string sql, SqlTransaction tran)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd = conn.CreateCommand();
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch(Exception e){
                throw new Exception($"错误Sql:{sql} 错误原因{e.Message}");
            }
            return result;
        }

        public int ExecuteUpdateObject(SqlConnection conn, object obj, SqlTransaction tran)
        {
            return ExecuteUpdate(conn, obj as string, tran);
        }
    }
}
