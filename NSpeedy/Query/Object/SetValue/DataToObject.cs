using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace NSpeedy.Query.Object.SetValue
{
    public interface DataToObject
    {
        IEnumerable<Dictionary<string, object>> Serialize(IDataReader reader);
        void SetObject<T>(T t, IDataReader dr, Dictionary<TableField, PropertyInfo> relation) where T : BasicObj;
        dynamic SetDynamicObject(IDataReader dr, IList<string> fields, Dictionary<TableField, PropertyInfo> relation);
    }
}
