using NSpeedy.Object.Basic;
using System.Collections.Generic;

namespace NSpeedy.Manager.Basic
{
    public interface MultipleOperation
    {
        void AddInsert<T>(T obj) where T : IdentityObj;
        void AddUpdate<T>(T obj) where T : IdentityObj;
        void AddDelete<T>(T obj) where T : IdentityObj;

        void AddExecutesql(string sql);
        void AddInsertBatchSame<T>(IList<T> list) where T : IdentityObj;
        void AddUpdateBatchSame<T>(IList<T> list) where T : IdentityObj;
        void AddDeleteBatchSame<T>(IList<T> obj) where T : IdentityObj;
        int Commit();
    }
}
