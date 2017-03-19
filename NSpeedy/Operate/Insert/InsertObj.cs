using System;
using System.Data.SqlClient;
using NSpeedy.Object.Basic;
using NSpeedy.Operate.Basic;

namespace NSpeedy.Operate.Insert
{
    public class InsertObj : ExecuteSingle
    {
        private BasicExecute insert;

        public InsertObj()
        {
            insert = new Basic.Insert();
        }

        public int ExecuteUpdate<T>(SqlConnection conn, T t) where T : IdentityObj
        {
            int result = -1;
            try
            {
                result = insert.ExecuteUpdate(conn, t, null);
            }
            catch (Exception e)
            {
                throw new Exception($"类:{this.GetType().ToString()}异常,原因:{e.Message}");
            }
            return result;
        }
    }
}
