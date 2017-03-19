using System;
using System.Data.SqlClient;
using NSpeedy.Operate.Basic;

namespace NSpeedy.Operate.Execute
{
    public class ExecuteSql: ExecuteSingleSql
    {
        private BasicExecuteSql execute;

        public ExecuteSql()
        {
            execute = new Basic.Execute();
        }

        public int ExecuteUpdate(SqlConnection conn, string sql) 
        {
            int result = -1;
            try
            {
                result = execute.ExecuteUpdate(conn, sql, null);
            }
            catch (Exception e)
            {
                throw new Exception($"类:{this.GetType().ToString()}异常,原因:{e.Message}");
            }
            return result;
        }
    }
}
