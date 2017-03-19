using NSpeedy.Query.Behavior;
using System;
using System.Collections.Generic;
using System.Text;
using NSpeedy.Object.Basic;
using System.Data.SqlClient;
using System.Data;
using NSpeedy.Query.Execute;
using NSpeedy.Query.Object.SetValue;

namespace NSpeedy.Query.Object.Single
{
    public class PreparedSelectSingle : QueryDataObject, QuerySingle
    {
        public override void Initialize()
        {
            this.queryData = new PreparedExecute();
            this.resultToObject = new ReflectObject();
        }

        public T SelectOne<T>(SqlConnection conn, T t) where T : IdentityObj
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from {0} where {1} =", t.GetTable(), t.GetTableKey());
            if ("int".Equals(t.GetTableKeyType()))
            {
                sql.AppendFormat(" {0}", t.ID.ToString());
            }
            else
            {
                sql.AppendFormat(" '{0}'", t.ID.ToString());
            }
            try
            {

                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {
                    List<string> fields = this.queryData.GetTableFields(dr);
                    if (dr.Read())
                    {
                        resultToObject.SetObject<T>(t, dr, t.GetRelation());
                    }
                }
            }
            catch (Exception e)
            {
                t = null;
                throw new Exception(string.Format("类:{0}异常,原因:{1},SQL语句:{2}", this.GetType().ToString(), e.Message, sql.ToString()));
            }
            finally
            {
                queryData.Close();
            }
            return t;
        }

        public T SelectOne<T>(SqlConnection conn, string sql) where T : BasicObj
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {

                    List<string> fields = this.queryData.GetTableFields(dr);
                    if (dr.Read())
                    {
                        resultToObject.SetObject<T>(t, dr, t.GetRelation(fields));
                    }
                    else
                    {
                        t = null;
                    }
                }
            }
            catch (Exception e)
            {
                t = null;
                throw new Exception(string.Format("类:{0}异常,原因:{1},SQL语句:{2}", this.GetType().ToString(), e.Message, sql.ToString()));
            }
            finally
            {
                queryData.Close();
            }
            return t;
        }

        public IDictionary<string, object> SelectOneData(SqlConnection conn, string sql)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {

                    List<string> fields = this.queryData.GetTableFields(dr);
                    if (dr.Read())
                    {
                        foreach (string f in fields)
                        {
                            dic.Add(f, dr[f]);
                        }
                    }
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
            return dic;
        }

        public T SelectRelyOnKey<T>(SqlConnection conn, string id) where T : BasicObj
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from {0} where {1} = '{2}'", t.GetTable(), t.GetTableKey(), id);
            //sql.AppendFormat(" '{0}'", id);
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {
                    List<string> fields = this.queryData.GetTableFields(dr);
                    if (dr.Read())
                    {
                        resultToObject.SetObject<T>(t, dr, t.GetRelation());
                    }
                    else
                    {
                        t = null;
                    }
                }
            }
            catch (Exception e)
            {
                t = null;
                throw new Exception(string.Format("类:{0}异常,原因:{1},SQL语句:{2}", this.GetType().ToString(), e.Message, sql.ToString()));
            }
            finally
            {
                queryData.Close();
            }
            return t;
        }
    }
}
