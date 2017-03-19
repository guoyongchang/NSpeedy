using NSpeedy.Annotate;
using System.Collections.Generic;
using System.Reflection;

namespace NSpeedy.Object.Basic.Behavior
{
    public interface TableRelation : Table, TableKey
    {
        Dictionary<TableField, PropertyInfo> GetRelation();
    }
}
