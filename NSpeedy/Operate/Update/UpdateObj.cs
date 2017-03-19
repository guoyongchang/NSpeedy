using System;
using System.Data.SqlClient;
using NSpeedy.Object.Basic;
using NSpeedy.Operate.Basic;

namespace NSpeedy.Operate.Update
{
    public class UpdateObj : ExecuteSingle
    {
        private BasicExecute update;

        public UpdateObj()
        {
            update = new Basic.Update();
        }
        public int ExecuteUpdate<T>(SqlConnection conn, T t) where T : IdentityObj
        {

            int result = -1;
            try
            {
                result = update.ExecuteUpdate(conn, t, null);
            }
            catch (Exception e)
            {
                throw new Exception($"类:{this.GetType().ToString()}异常,原因:{e.Message}");
            }
            return result;
        }
    }
}
