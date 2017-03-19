using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NSpeedy.Query.Behavior;

namespace NSpeedy.Query.Execute
{
    public abstract class ExecuteQuery : QueryData
    {
        public abstract void Close();
        public abstract IDataReader GetResult(SqlConnection conn, string ps);
        public List<string> GetTableFields(IDataReader dr)
        {
            List<string> fields = new List<string>();
            var columns = dr.GetSchemaTable().Columns;
            for (int i = 0; i < dr.FieldCount; i++)
            {
                fields.Add(dr.GetName(i).Trim());
            }
            return fields;
        }
    }
}
