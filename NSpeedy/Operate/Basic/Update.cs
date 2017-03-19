using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using NSpeedy.Operate.Basic.Interface;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using NSpeedy.Operate.Sql;
using System;

namespace NSpeedy.Operate.Basic
{
    public class Update : BasicExecute, BasicExecuteObject
    {
        private object SqlNull(object obj)
        {
            if (obj == null)
                return DBNull.Value;
            return obj;
        }
        public int ExecuteUpdate<T>(SqlConnection conn, T obj, SqlTransaction tran) where T : IdentityObj
        {
            CreateSql createsql = new CreateUpdateSql();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Dictionary<TableField, PropertyInfo> fields = obj.GetRelation();
            foreach (var item in fields)
            {
                sqlParams.Add(new SqlParameter(string.Format("@{0}", item.Key.Name), SqlNull(item.Value.GetValue(obj))));
            }
            SqlCommand cmd = conn.CreateCommand();
            cmd = conn.CreateCommand();
            cmd.Transaction = tran;
            cmd.Parameters.AddRange(sqlParams.ToArray());
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                cmd.Parameters[i].IsNullable = true;
            }
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = createsql.Create<T>(obj);
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return result;
        }

        public int ExecuteUpdateObject(SqlConnection conn, object obj, SqlTransaction tran)
        {
            return ExecuteUpdate(conn, obj as IdentityObj, tran);
        }
    }
}
