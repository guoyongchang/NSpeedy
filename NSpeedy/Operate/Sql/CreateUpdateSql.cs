using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NSpeedy.Operate.Sql
{
    public class CreateUpdateSql : CreateSql
    {
        public string Create<T>(T t) where T : IdentityObj
        {
            //T t = (T)Activator.CreateInstance(typeof(T));
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update {0} set ", t.GetTable());
            Dictionary<TableField, PropertyInfo> fields = t.GetRelation();
            fields = (from c in fields where c.Key.Own.ToUpper().Equals("Y") select c).ToDictionary(k => k.Key, v => v.Value);
            fields.Remove(new TableField() { Own = "Y", Name = "ID" });
            foreach (var item in fields)
            {
                sql.AppendFormat("{0}=@{0},", item.Key.Name);
            }
            sql.Remove(sql.Length - 1, 1);
            sql.AppendFormat(" where {0} = @{0}", t.GetTableKey());
            return sql.ToString();
        }
    }
}