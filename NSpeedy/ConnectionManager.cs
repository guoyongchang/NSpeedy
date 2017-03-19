using System.Data.SqlClient;

namespace NSpeedy
{
    public interface ConnectionManager
    {
        SqlConnection GetConnection();
        void CloseConn(SqlConnection conn);
    }
}
