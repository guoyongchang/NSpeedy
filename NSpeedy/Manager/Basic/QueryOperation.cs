using NSpeedy.Object.Basic;
using System.Collections.Generic;
using System.Data;

namespace NSpeedy.Manager.Basic
{
    public interface QueryOperation
    {
        T SelectRelyOnKey<T>(string id) where T : BasicObj;
        T SelectOne<T>(string sql) where T : BasicObj;
        T SelectOne<T>(T t) where T : IdentityObj;
        T SelectValue<T>(string sql);
        string SelectWithJSON(string sql);
        DataSet SelectWithDataSet(string sql, params string[] tableName);
        IList<T> Select<T>(string sql) where T : BasicObj;
        IList<T> Select<T>(string sql, object obj) where T : BasicObj;
        IList<dynamic> SelectWithDynamic<T>(string sql) where T : BasicObj;
        IList<dynamic> SelectWithDynamic(string sql);
        IList<T> SelectValueList<T>(string sql);
        IDictionary<string, object> SelectOneData(string sql);
    }
}
