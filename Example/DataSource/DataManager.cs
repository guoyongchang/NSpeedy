using NSpeedy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Example.DataSource
{
    public class DataManager : NSpeedy.Manager.AbstractDataOperater
    {
        private static DBConnectionSingletion pool = DBConnectionSingletion.Instance;
        public override void CloseConn(SqlConnection conn)
        {
            pool.ReturnDBConnection(conn);
        }

        public override SqlConnection GetConnection()
        {
            SqlConnection SqlConn = pool.BorrowDBConnection();
            if (SqlConn.State == System.Data.ConnectionState.Open)
                SqlConn.Close();
            SqlConn.Open();
            return SqlConn;
        }
    }
}
