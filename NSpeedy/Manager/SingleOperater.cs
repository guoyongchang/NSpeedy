using NSpeedy.Manager.Basic;
using NSpeedy.Object.Basic;
using NSpeedy.Operate;
using NSpeedy.Operate.Delete;
using NSpeedy.Operate.Execute;
using NSpeedy.Operate.Insert;
using NSpeedy.Operate.Update;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Manager
{
    public class SingleOperater : BasicOperater, Basic.SingleOperation
    {
        private ExecuteSingle insertObj;
        private ExecuteSingle deleteObj;
        private ExecuteSingle updateObj;
        private ExecuteBatch insertSameBatch;
        private ExecuteSingleSql executeSql;
        private ExecuteBatchSql executeBatchSql;
        public SingleOperater(ConnectionManager cm) : base(cm)
        {
            insertObj = new InsertObj();
            deleteObj = new DeleteObj();
            updateObj = new UpdateObj();
            insertSameBatch = new InsertSameObjs();
            executeSql = new ExecuteSql();
            executeBatchSql = new ExecuteMoreSql();
        }
        public int Delete<T>(T obj) where T : IdentityObj
        {
            SqlConnection conn = cm.GetConnection();
            int i = deleteObj.ExecuteUpdate(conn, obj);
            cm.CloseConn(conn);
            return i;
        }

        public int DeleteBatchSame<T>(IList<T> list) where T : IdentityObj
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T obj) where T : IdentityObj
        {
            SqlConnection conn = cm.GetConnection();
            int i = insertObj.ExecuteUpdate(conn, obj);
            cm.CloseConn(conn);
            return i;
        }
        public int InsertBatchSame<T>(IList<T> list) where T : IdentityObj
        {
            SqlConnection conn = cm.GetConnection();
            int i = insertSameBatch.ExecuteUpdate(conn, list);
            cm.CloseConn(conn);
            return i;
        }

        public int Update<T>(T obj) where T : IdentityObj
        {
            SqlConnection conn = cm.GetConnection();
            int i = updateObj.ExecuteUpdate(conn, obj);
            cm.CloseConn(conn);
            return i;
        }
        public int UpdateBatchSame<T>(IList<T> list) where T : IdentityObj
        {
            throw new NotImplementedException();
        }
        public int ExecuteSql(string sql)
        {
            SqlConnection conn = cm.GetConnection();
            int i = executeSql.ExecuteUpdate(conn, sql);
            cm.CloseConn(conn);
            return i;
        }
        public int ExecuteMoreSql(IList<string> list)
        {
            SqlConnection conn = cm.GetConnection();
            int i = executeBatchSql.ExecuteUpdate(conn, list);
            cm.CloseConn(conn);
            return i;
        }
    }
}
