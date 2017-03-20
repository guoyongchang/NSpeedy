using System;
using System.Data;
using System.Data.SqlClient;

namespace Example.DataSource
{
    public sealed class DBConnectionSingletion : ObjectPool
    {
        private DBConnectionSingletion() { }

        public static readonly DBConnectionSingletion Instance = new DBConnectionSingletion();

        private static string connectionString = "链接字符串";
        //例:"Data Source=127.0.0.1;Initial Catalog=TestTable;User ID=sa;Password=xxx;pooling=true;min pool size=5;max pool size=4000;connect timeout =180;";
        //例:ConfigurationManager.ConnectionStrings["ConnectionString"].ToString().Trim();

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        protected override object Create()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected override bool Validate(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        protected override void Expire(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                conn.Close();
            }
            catch (SqlException) { }
        }

        public SqlConnection BorrowDBConnection()
        {
            try
            {
                return (SqlConnection)base.GetObjectFromPool();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReturnDBConnection(SqlConnection conn)
        {
            base.ReturnObjectToPool(conn);
        }
    }
}
