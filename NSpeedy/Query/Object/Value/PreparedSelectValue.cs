using System;
using System.Data.SqlClient;
using NSpeedy.Query.Behavior;
using System.Data;
using NSpeedy.Query.Object.Single;
using NSpeedy.Query.Execute;
using NSpeedy.Query.Object.SetValue;
using System.Collections.Generic;

namespace NSpeedy.Query.Object.Value
{
    public class PreparedSelectValue : QueryDataObject, QueryValue
    {
        public override void Initialize()
        {
            this.queryData = new PreparedExecute();
            this.resultToObject = new ReflectObject();
        }

        public T SelectValue<T>(SqlConnection conn, string sql)
        {
            IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
            object Result = null;
            if (dr.Read())
            {
                try
                {
                    Result = dr[0];
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("类:{0}异常,原因:{1},SQL语句:{2}", this.GetType().ToString(), e.Message, sql.ToString()));
                }
                finally
                {
                    queryData.Close();
                }
            }
            return (T)Result;
        }

        public IList<T> SelectValueList<T>(SqlConnection conn, string sql)
        {
            IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
            IList<T> list = new List<T>();
            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        list.Add((T)dr[0]);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("类:{0}异常,原因:{1},SQL语句:{2}", this.GetType().ToString(), e.Message, sql.ToString()));
                }
                finally
                {
                    queryData.Close();
                }
            }
            return list;
        }
    }
}
