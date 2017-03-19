using System.Data.SqlClient;

namespace NSpeedy.Manager.Basic
{
    public abstract class BasicOperater : ConnectionManager
    {
        protected ConnectionManager cm;

        public BasicOperater(ConnectionManager cm)
        {
            this.cm = cm;
        }
        public void CloseConn(SqlConnection conn)
        {
            cm.CloseConn(conn);
        }

        public SqlConnection GetConnection()
        {
            return cm.GetConnection();
        }
    }
}
