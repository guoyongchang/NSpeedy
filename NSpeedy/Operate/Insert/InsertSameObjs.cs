using System;
using System.Data.SqlClient;
using NSpeedy.Object.Basic;
using System.Collections.Generic;
using NSpeedy.Operate.Basic;

namespace NSpeedy.Operate.Insert
{
    public class InsertSameObjs : ExecuteBatch
    {
        private BasicExecuteMore executeBatch;

        public InsertSameObjs()
        {
            executeBatch = new Basic.InsertMore();
        }

        public int ExecuteUpdate<T>(SqlConnection conn, IList<T> t) where T : IdentityObj
        {

            int result = -1;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlTransaction = conn.BeginTransaction();
                result = executeBatch.ExecuteUpdate(conn, t, sqlTransaction);
            }
            catch (Exception e)
            {
                sqlTransaction.Commit();

                throw new Exception($"类:{this.GetType().ToString()}异常,原因:{e.Message}");
            }
            return result;
        }
    }
}
