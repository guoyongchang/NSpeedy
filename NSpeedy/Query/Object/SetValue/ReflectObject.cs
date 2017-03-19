using System;
using System.Collections.Generic;
using System.Data;
using NSpeedy.Object.Basic;
using System.Reflection;
using NSpeedy.Annotate;
using System.Linq;

namespace NSpeedy.Query.Object.SetValue
{
    public class ReflectObject : DataToObject
    {
        public void SetObject<T>(T t, IDataReader dr, Dictionary<TableField, PropertyInfo> relation) where T : BasicObj
        {
            foreach (var item in relation)
            {
                if (!string.IsNullOrEmpty(dr[item.Key.Name].ToString()))
                    item.Value.SetValue(t, dr[item.Key.Name]);
            }
        }
        public IEnumerable<Dictionary<string, object>> Serialize(IDataReader reader)
        {
            var columns = new List<string>();
            var rows = new List<Dictionary<string, object>>();

            do
            {
                columns = new List<string>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    columns.Add(reader.GetName(i));
                }

                while (reader.Read())
                {
                    rows.Add(columns.ToDictionary(column => column, column => reader[column]));
                }
            }
            while (reader.NextResult());
            return rows;
        }
        public dynamic SetDynamicObject(IDataReader dr, IList<string> fields, Dictionary<TableField, PropertyInfo> relation)
        {
            dynamic dobj = new System.Dynamic.ExpandoObject();
            var dic = (IDictionary<string, object>)dobj;
            foreach (var item in relation)
            {
                dic[item.Value.Name] = dr[item.Key.Name];
            }
            foreach (var lostField in fields)
            {
                dic[lostField] = dr[lostField];
            }
            return dobj;
        }
    }
}
