using System.Collections.Generic;
using System.Data.SqlClient;
using NSpeedy.Object.Basic;
using System.Data;
using NSpeedy.Operate.Basic.Interface;
using System.Collections;

namespace NSpeedy.Operate.Basic
{
    public class DeleteMore : BasicExecuteMore, BasicExecuteObject
    {
        public int ExecuteUpdate<T>(SqlConnection conn, IList<T> objs, SqlTransaction tran) where T : IdentityObj
        {
            if (objs.Count == 0)
                return 0;
            T t = objs[0];//(T)Activator.CreateInstance(typeof(T));
            DataTable dt = new DataTable();
            dt.Columns.Add(t.GetTableKey(), typeof(object));
            dt.PrimaryKey = new DataColumn[] { dt.Columns[t.GetTableKey()] };
            foreach (T obj in objs)
            {
                DataRow dr = dt.NewRow();
                if (obj.ID == null)
                    continue;
                else
                {
                    dr[t.GetTableKey()] = obj.ID;
                }
                dt.Rows.Add(dr);
            }
            //using (BulkOperation bulk = new BulkOperation(conn))
            //{
            //    bulk.Transaction = tran;
            //    bulk.BatchSize = 300;
            //    bulk.DestinationTableName = t.GetTable();
            //    bulk.BulkDelete(dt);
            //}
            return dt.Rows.Count;
        }

        public int ExecuteUpdateObject(SqlConnection conn, object obj, SqlTransaction tran)
        {
            IList list = (IList)obj;
            List<IdentityObj> l = new List<IdentityObj>();
            foreach (object o in list)
            {
                l.Add((IdentityObj)o);
            }

            return ExecuteUpdate(conn, l, tran);
            //return ExecuteUpdate(conn, obj as IList<IdentityObj>, tran);
        }
    }
}
