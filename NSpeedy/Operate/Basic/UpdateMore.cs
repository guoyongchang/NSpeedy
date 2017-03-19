using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using NSpeedy.Operate.Basic.Interface;
using NSpeedy.Operate.Sql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace NSpeedy.Operate.Basic
{
    public class UpdateMore : BasicExecuteMore, BasicExecuteObject
    {

        private static Dictionary<Type, DbType> dbtypeMap = new Dictionary<Type, DbType>();
        static void InitDbTypeMap()
        {
            dbtypeMap[typeof(byte)] = DbType.Byte;
            dbtypeMap[typeof(sbyte)] = DbType.SByte;
            dbtypeMap[typeof(short)] = DbType.Int16;
            dbtypeMap[typeof(ushort)] = DbType.UInt16;
            dbtypeMap[typeof(int)] = DbType.Int32;
            dbtypeMap[typeof(uint)] = DbType.UInt32;
            dbtypeMap[typeof(long)] = DbType.Int64;
            dbtypeMap[typeof(ulong)] = DbType.UInt64;
            dbtypeMap[typeof(float)] = DbType.Single;
            dbtypeMap[typeof(double)] = DbType.Double;
            dbtypeMap[typeof(Decimal)] = DbType.Decimal;
            dbtypeMap[typeof(bool)] = DbType.Boolean;
            dbtypeMap[typeof(string)] = DbType.String;
            dbtypeMap[typeof(char)] = DbType.StringFixedLength;
            dbtypeMap[typeof(Guid)] = DbType.Guid;
            dbtypeMap[typeof(DateTime)] = DbType.DateTime;
            dbtypeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            dbtypeMap[typeof(TimeSpan)] = DbType.Time;
            dbtypeMap[typeof(byte[])] = DbType.Binary;
            dbtypeMap[typeof(byte?)] = DbType.Byte;
            dbtypeMap[typeof(sbyte?)] = DbType.SByte;
            dbtypeMap[typeof(short?)] = DbType.Int16;
            dbtypeMap[typeof(ushort?)] = DbType.UInt16;
            dbtypeMap[typeof(int?)] = DbType.Int32;
            dbtypeMap[typeof(uint?)] = DbType.UInt32;
            dbtypeMap[typeof(long?)] = DbType.Int64;
            dbtypeMap[typeof(ulong?)] = DbType.UInt64;
            dbtypeMap[typeof(float?)] = DbType.Single;
            dbtypeMap[typeof(double?)] = DbType.Double;
            dbtypeMap[typeof(Decimal?)] = DbType.Decimal;
            dbtypeMap[typeof(bool?)] = DbType.Boolean;
            dbtypeMap[typeof(char?)] = DbType.StringFixedLength;
            dbtypeMap[typeof(Guid?)] = DbType.Guid;
            dbtypeMap[typeof(DateTime?)] = DbType.DateTime;
            dbtypeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
            dbtypeMap[typeof(TimeSpan?)] = DbType.Time;
            dbtypeMap[typeof(object)] = DbType.Object;
        }

        public int ExecuteUpdate<T>(SqlConnection conn, IList<T> objs, SqlTransaction tran) where T : IdentityObj
        {
            InitDbTypeMap();
            if (objs.Count == 0)
                return 0;
            T t = objs[0];
            SqlDataAdapter sd = new SqlDataAdapter();
            CreateSql createsql = new CreateUpdateSql();
            sd.InsertCommand = new SqlCommand(createsql.Create<T>(t), conn, tran);
            sd.UpdateCommand = new SqlCommand(createsql.Create<T>(t), conn, tran);
            DataTable dt = new DataTable();
            Dictionary<TableField, PropertyInfo> fields = t.GetRelation();
            foreach (var field in fields)
            {
                dt.Columns.Add(field.Key.Name, field.Value.PropertyType);//.Add(new SqlParameter(string.Format("@{0}", item.Key.Name), item.Value.GetValue(obj)));
                //.Add(string.Format("@{0}", field.Key.Name), field.Value.PropertyType,16);
                //sd.UpdateCommand.Parameters.Add(string.Format("@{0}", field.Key.Name), dbtypeMap[field.Value.PropertyType]);
                //sd.InsertCommand.Parameters.Add(string.Format("@{0}", field.Key.Name), dbtypeMap[field.Value.PropertyType]);
            }
            dt.Columns.Add(t.GetTableKey(), typeof(object));
            dt.PrimaryKey = new DataColumn[] { dt.Columns[t.GetTableKey()] };
            //sd.UpdateCommand.Parameters.Add(string.Format("@{0}", t.GetTableKey()), dbtypeMap[typeof(object)]);
            //sd.InsertCommand.Parameters.Add(string.Format("@{0}", t.GetTableKey()), dbtypeMap[typeof(object)]);
            //sd.UpdateCommand.Parameters.Add(new SqlParameter(string.Format("@{0}", t.GetTableKey()),typeof(object)));
            foreach (T obj in objs)
            {
                DataRow dr = dt.NewRow();
                foreach (var item in fields)
                {
                    dr[item.Key.Name] = item.Value.GetValue(obj);
                }
                dr[t.GetTableKey()] = obj.ID;
                dt.Rows.Add(dr);
            }
            //sd.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
            sd.Update(dt);
            //using (BulkOperation bulk = new BulkOperation(conn))
            //{
            //    bulk.Transaction = tran;
            //    bulk.BatchSize = 300;
            //    bulk.DestinationTableName = t.GetTable();
            //    bulk.BulkUpdate(dt);
            //}
            return objs.Count;
        }

        public int ExecuteUpdateObject(SqlConnection conn, object obj, SqlTransaction tran)
        {
            IList list = (IList)obj;
            List<IdentityObj> l = new List<IdentityObj>();
            foreach (object o in list)
            {
                l.Add((IdentityObj)o);
            }
            return ExecuteUpdate(conn, l, tran);
        }
    }
}
