using System.Collections.Generic;
using System.Data.SqlClient;
using NSpeedy.Object.Basic;
using System.Data;
using NSpeedy.Operate.Basic.Interface;
using System;
using NSpeedy.Operate.Sql;

namespace NSpeedy.Operate.Basic
{
    public class Delete : BasicExecute, BasicExecuteObject
    {
        public int ExecuteUpdate<T>(SqlConnection conn, T obj, SqlTransaction tran) where T : IdentityObj
        {
            CreateSql createSql = new CreateDeleteSql();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter(string.Format("@{0}", obj.GetTableKey()), obj.ID));
            SqlCommand cmd = conn.CreateCommand();
            cmd = conn.CreateCommand();
            cmd.Parameters.AddRange(sqlParams.ToArray());
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = createSql.Create<T>(obj);
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return result;
        }

        public int ExecuteUpdateObject(SqlConnection conn, object obj,SqlTransaction tran)
        {
            return ExecuteUpdate(conn, obj as IdentityObj, tran);
        }
    }
}
