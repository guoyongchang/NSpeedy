using System.Data;
using System.Data.SqlClient;

namespace NSpeedy.Query.Execute
{
    public class PreparedExecute : ExecuteQuery
    {
        SqlCommand cmd;
        IDataReader dr;
        public override void Close()
        {
            try
            {
                if (dr != null)
                {
                    dr.Dispose();
                    dr.Close();
                }
            }
            catch { }
            try
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            catch { }
        }

        public override IDataReader GetResult(SqlConnection conn, string ps)
        {
            //SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
            //根据sql获取dr返回
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ps;
            //cmd.Transaction = tran;
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch
            {
                dr = null;
            }
            finally
            {
                //tran.Commit();
                //tran.Dispose();
                cmd.Dispose();
            }

            return dr;
        }
    }
}
