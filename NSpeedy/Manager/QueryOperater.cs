using NSpeedy.Manager.Basic;
using NSpeedy.Query.Behavior;
using NSpeedy.Query.Object.Single;
using System;
using System.Collections.Generic;
using NSpeedy.Object.Basic;
using System.Data.SqlClient;
using NSpeedy.Query.Object.Value;
using NSpeedy.Query.Object.More;
using System.Data;
using NSpeedy.Test;

namespace NSpeedy.Manager
{
    public class QueryOperater : BasicOperater, Basic.QueryOperation
    {
        //private QuerySingle querySingle;
        //private QueryValue queryValue;
        //private QueryMore queryMore;
        public QueryOperater(ConnectionManager cm) : base(cm)
        {
            //querySingle = new PreparedSelectSingle();
            //queryValue = new PreparedSelectValue();
            //queryMore = new PreparedSelectMore();
        }
        public IList<dynamic> SelectWithDynamic(string sql)
        {
            SqlConnection conn = cm.GetConnection();
            QueryMore queryMore = new PreparedSelectMore();
            IList<dynamic> list = queryMore.SelectWithDynamic<Entity>(conn, sql);
            return list;
        }
        public IList<dynamic> SelectWithDynamic<T>(string sql) where T : BasicObj
        {
            SqlConnection conn = cm.GetConnection();
            QueryMore queryMore = new PreparedSelectMore();
            IList<dynamic> list = queryMore.SelectWithDynamic<T>(conn, sql);
            return list;
        }
        public IList<T> Select<T>(string sql, object obj) where T : BasicObj
        {
            SqlConnection conn = cm.GetConnection();
            QueryMore queryMore = new PreparedSelectMore();
            IList<T> list = queryMore.Select<T>(conn, sql, obj);
            cm.CloseConn(conn);
            return list;
        }
        ///
        public string SelectWithJSON(string sql)
        {
            SqlConnection conn = cm.GetConnection();
            QueryMore queryMore = new PreparedSelectMore();
            string result = queryMore.SelectWithJSON(conn, sql);
            cm.CloseConn(conn);
            return result;
        }
        public DataSet SelectWithDataSet(string sql, params string[] tableName)
        {
            SqlConnection conn = cm.GetConnection();
            QueryMore queryMore = new PreparedSelectMore();
            DataSet result = queryMore.SelectWithDataSet(conn, sql, tableName);
            cm.CloseConn(conn);
            return result;
        }

        public IList<T> Select<T>(string sql) where T : BasicObj
        {
            QueryMore queryMore = new PreparedSelectMore();
            SqlConnection conn = cm.GetConnection();
            IList<T> list = queryMore.Select<T>(conn, sql);
            cm.CloseConn(conn);
            return list;
        }

        public T SelectOne<T>(T t) where T : IdentityObj
        {
            SqlConnection conn = cm.GetConnection();
            QuerySingle querySingle = new PreparedSelectSingle();
            T t1 = querySingle.SelectOne<T>(conn, t);
            cm.CloseConn(conn);
            return t1;
        }

        public T SelectOne<T>(string sql) where T : BasicObj
        {
            SqlConnection conn = cm.GetConnection();
            QuerySingle querySingle = new PreparedSelectSingle();
            T t1 = querySingle.SelectOne<T>(conn, sql);
            cm.CloseConn(conn);
            return t1;
        }

        public T SelectRelyOnKey<T>(string id) where T : BasicObj
        {
            SqlConnection conn = cm.GetConnection();
            QuerySingle querySingle = new PreparedSelectSingle();
            T t1 = querySingle.SelectRelyOnKey<T>(conn, id);
            cm.CloseConn(conn);
            return t1;
        }

        public T SelectValue<T>(string sql)
        {
            SqlConnection conn = cm.GetConnection();
            QueryValue queryValue = new PreparedSelectValue();
            T t1 = queryValue.SelectValue<T>(conn, sql);
            cm.CloseConn(conn);
            return t1;
        }

        public IList<T> SelectValueList<T>(string sql)
        {
            SqlConnection conn = cm.GetConnection();
            QueryValue queryValue = new PreparedSelectValue();
            IList<T> list = queryValue.SelectValueList<T>(conn, sql);
            cm.CloseConn(conn);
            return list;
        }

        public IDictionary<string, object> SelectOneData(string sql)
        {
            SqlConnection conn = cm.GetConnection();
            QuerySingle querySingle = new PreparedSelectSingle();
            IDictionary<string, object> dic = querySingle.SelectOneData(conn, sql);
            cm.CloseConn(conn);
            return dic;
        }
    }
}
