using System;
using System.Data.SqlClient;
using NSpeedy.Object.Basic;

namespace NSpeedy.Operate.Delete
{
    public class DeleteObj : ExecuteSingle
    {

        public int ExecuteUpdate<T>(SqlConnection conn, T t) where T : IdentityObj
        {
            int result = -1;
            string sql = null;
            try
            {
                result = new Basic.Update().ExecuteUpdate(conn, t, null);
            }
            catch (Exception e)
            {
                throw new Exception($"类:{this.GetType().ToString()}异常,原因:{e.Message},SQL语句:{sql}");
            }
            return result;
        }
    }
}
