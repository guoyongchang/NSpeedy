using NSpeedy.Manager.Basic;
using NSpeedy.Object.Basic;
using NSpeedy.Operate.Multi;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Manager
{
    public class MultipleOperater : BasicOperater, MultipleOperation
    {
        private IDictionary<string, object> dic;
        private ProcessingBatch pb;
        public MultipleOperater(ConnectionManager cm, IDictionary<string, object> dictionary) : base(cm)
        {
            //dic = new Dictionary<string, object>();
            this.dic = dictionary;
            pb = new CommonProcessingBatch();
        }

        public void AddDelete<T>(T obj) where T : IdentityObj
        {
            dic.Add("d_" + dic.Count + 1, obj);
        }
        public void AddDeleteBatchSame<T>(IList<T> obj) where T : IdentityObj
        {
            dic.Add("dbs_" + dic.Count + 1, obj);
        }

        public void AddExecutesql(string sql)
        {
            dic.Add("e_" + dic.Count + 1, sql);
        }

        public void AddInsert<T>(T obj) where T : IdentityObj
        {
            dic.Add("i_" + dic.Count + 1, obj);
        }

        public void AddInsertBatchSame<T>(IList<T> list) where T : IdentityObj
        {
            dic.Add("ibs_" + dic.Count + 1, list);
        }

        public void AddUpdate<T>(T obj) where T : IdentityObj
        {
            dic.Add("u_" + dic.Count + 1, obj);
        }

        public void AddUpdateBatchSame<T>(IList<T> list) where T : IdentityObj
        {
            dic.Add("ubs_" + dic.Count + 1, list);
        }

        public int Commit()
        {
            SqlConnection conn = cm.GetConnection();
            int i = pb.ExecuteUpdate(conn, dic);
            cm.CloseConn(conn);
            dic.Clear();
            return i;
        }
    }
}
