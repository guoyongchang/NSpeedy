using NSpeedy.Operate.Basic;
using NSpeedy.Operate.Basic.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Operate.Multi
{
    public class CommonProcessingBatch : ProcessingBatch
    {
        private IDictionary<string, BasicExecuteObject> dicy;
        public CommonProcessingBatch()
        {
            dicy = new Dictionary<string, BasicExecuteObject>();
            dicy.Add("i", new Basic.Insert());
            dicy.Add("u", new Basic.Update());
            dicy.Add("d", new Basic.Delete());
            dicy.Add("e", new Basic.Execute());
            dicy.Add("ibs", new InsertMore());
            dicy.Add("ubs", new UpdateMore());
            dicy.Add("dbs", new DeleteMore());


        }

        public int ExecuteUpdate(SqlConnection conn, IDictionary<string, object> dic)
        {
            int i = 0;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlTransaction = conn.BeginTransaction();
                BasicExecuteObject be = null;
                foreach (var item in dic)
                {
                    string type = item.Key.Split('_')[0];
                    be = dicy[type];
                    i += be.ExecuteUpdateObject(conn, item.Value, sqlTransaction);
                }
                sqlTransaction.Commit();
            }
            catch (Exception e)
            {
                sqlTransaction.Rollback();
                i = 0;
                throw new Exception(e.Message);
            }

            return i;
        }
    }
}
