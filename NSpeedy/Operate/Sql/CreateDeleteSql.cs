using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NSpeedy.Operate.Sql
{
    public class CreateDeleteSql : CreateSql
    {
        public string Create<T>(T t) where T : IdentityObj
        {
            //T t = (T)Activator.CreateInstance(typeof(T));
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("delete from {0} where {1} = @{1}", t.GetTable(),t.GetTableKey());
            return sql.ToString();
        }
    }
}
