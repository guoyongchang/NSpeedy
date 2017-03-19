using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using NSpeedy.Operate.Sql;
using NSpeedy.Operate.Basic.Interface;
using NSpeedy.Test;
using System;

namespace NSpeedy.Operate.Basic
{
    public class Insert : BasicExecute, BasicExecuteObject
    {
        private object SqlNull(object obj)
        {
            if (obj == null)
                return DBNull.Value;
            return obj;
        }
        public int ExecuteUpdate<T>(SqlConnection conn, T obj, SqlTransaction tran) where T : IdentityObj
        {
            CreateSql createsql = new CreateInsertSql();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Dictionary<TableField, PropertyInfo> fields = obj.GetRelation();
            //fields = (from c in fields where c.Value.GetValue(obj) != null select c).ToDictionary(k => k.Key, v => v.Value);
            foreach (var item in fields)
            {
                if (item.Key.Name.ToUpper().Equals(obj.GetTableKey().ToUpper()))
                {
                    object id = item.Value.GetValue(obj);
                    if (id == null)
                        id = obj.GetAutoID();
                    sqlParams.Add(new SqlParameter(string.Format("@{0}", obj.GetTableKey()), id));
                    continue;
                }
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
