using Newtonsoft.Json;
using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using NSpeedy.Query.Behavior;
using NSpeedy.Query.Execute;
using NSpeedy.Query.Object.SetValue;
using NSpeedy.Query.Object.Single;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace NSpeedy.Query.Object.More
{
    public class PreparedSelectMore : QueryDataObject, QueryMore
    {
        #region 垃圾代码
        // MethodInfo methodInfo = typeof(T).GetMethod("SetObject");
        // FastInvoke.FastInvokeHandler fastInvoker = FastInvoke.GetMethodInvoker(methodInfo);
        // IList<object> obj = new List<object>();
        //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        //stopwatch.Start();
        //Record r = new Record();
        //obj.Add(r);// (T)t.Clone();
        //fastInvoker(newT, new object[] { dr });
        //stopwatch.Stop();
        //Console.WriteLine(stopwatch.ElapsedMilliseconds + "Ticks");
        #endregion
        public override void Initialize()
        {
            this.queryData = new PreparedExecute();
            this.resultToObject = new ReflectObject();
        }
        public IList<T> Select<T>(SqlConnection conn, string sql, object obj) where T : BasicObj
        {
            T t = obj as T;
            //T t = (T)Activator.CreateInstance(typeof(T));
            IList<T> result = new List<T>();
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {
                    List<string> fields = this.queryData.GetTableFields(dr);
                    Dictionary<TableField, PropertyInfo> Relation = t.GetRelation(fields);
                    while (dr.Read())
                    {
                        T newT = (T)t.Clone();
                        //T newT = (T)Activator.CreateInstance(typeof(T));
                        resultToObject.SetObject<T>(newT, dr, Relation);
                        result.Add(newT);
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
            return result;
        }
        public IList<T> Select<T>(SqlConnection conn, string sql) where T : BasicObj
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            IList<T> result = new List<T>();
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {
                    List<string> fields = this.queryData.GetTableFields(dr);
                    Dictionary<TableField, PropertyInfo> Relation = t.GetRelation(fields);
                    while (dr.Read())
                    {
                        T newT = (T)Activator.CreateInstance(typeof(T));
                        resultToObject.SetObject<T>(newT, dr, Relation);
                        result.Add(newT);
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
            return result;
        }
        public string SelectWithJSON(SqlConnection conn, string sql)
        {
            string result = null;
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {
                    var r = resultToObject.Serialize(dr);
                    result = JsonConvert.SerializeObject(r, Formatting.None);
                }
            }
            catch (Exception e)
            {
                result = null;
                throw new Exception(string.Format("类:{0}异常,原因:{1},SQL语句:{2}", this.GetType().ToString(), e.Message, sql.ToString()));
            }
            finally
            {
                queryData.Close();
            }
            return result ?? "[]";
        }
        public DataSet SelectWithDataSet(SqlConnection conn, string sql, params string[] tableName)
        {
            DataSet result = new DataSet();
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                if (dr != null)
                {
                    result.Load(dr, LoadOption.Upsert, tableName);
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
            return result;
        }

        public IList<dynamic> SelectWithDynamic<T>(SqlConnection conn, string sql) where T : BasicObj
        {
            List<dynamic> dynList = new List<dynamic>();
            T t = (T)Activator.CreateInstance(typeof(T));
            try
            {
                IDataReader dr = this.queryData.GetResult(conn, sql.ToString());
                List<string> fields = this.queryData.GetTableFields(dr);
                Dictionary<TableField, PropertyInfo> Relation = t.GetRelation(fields);
                (from c in fields join p in Relation on c equals p.Key.Name select c).ToList().ForEach(x => fields.Remove(x));
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        dynList.Add(resultToObject.SetDynamicObject(dr, fields,Relation));
                    }
                }
            }catch (Exception e)
            {
                throw new Exception(string.Format("类:{0}异常,原因:{1},SQL语句:{2}", this.GetType().ToString(), e.Message, sql.ToString()));
            }
            return dynList;
        }
    }
}
