using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace NSpeedy.Operate.Sql
{
    public class CreateInsertSql : CreateSql
    {
        public string Create<T>(T t) where T : IdentityObj
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("insert into {0}(", t.GetTable());
            Dictionary<TableField, PropertyInfo> fields = t.GetRelation();
            fields = (from c in fields where c.Key.Own.ToUpper().Equals("Y")  /*&& c.Value.GetValue(t) != null*/ select c).ToDictionary(k => k.Key, v => v.Value);
            foreach (var item in fields)
            {
                sql.AppendFormat("{0},", item.Key.Name);
            }
            //sql.AppendFormat("{0},", t.GetTableKey());
            sql.Remove(sql.Length - 1, 1);
            sql.Append(")");
            sql.Append(" values(");
            foreach (var item in fields)
            {
                sql.AppendFormat("@{0},", item.Key.Name);
            }
            //sql.AppendFormat("@{0},", t.GetTableKey());
            sql.Remove(sql.Length - 1, 1);
            sql.Append(")");
            return sql.ToString();
        }
    }
}
