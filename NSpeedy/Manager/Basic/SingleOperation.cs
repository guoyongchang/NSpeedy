using NSpeedy.Object.Basic;
using System.Collections;
using System.Collections.Generic;

namespace NSpeedy.Manager.Basic
{
    public interface SingleOperation
    {
        int Insert<T>(T obj) where T : IdentityObj;

        int InsertBatchSame<T>(IList<T> list) where T : IdentityObj;

        int Update<T>(T obj) where T : IdentityObj;

        int UpdateBatchSame<T>(IList<T> list) where T : IdentityObj;

        int Delete<T>(T obj) where T : IdentityObj;

        int DeleteBatchSame<T>(IList<T> list) where T : IdentityObj;

        int ExecuteSql(string sql);
        int ExecuteMoreSql(IList<string> list);

    }
}
