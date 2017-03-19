using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using NSpeedy.Operate.Basic.Interface;
using NSpeedy.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace NSpeedy.Operate.Basic
{
    public class InsertMore : BasicExecuteMore, BasicExecuteObject
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
            if (objs.Count == 0)
            {
                return 0;
            }

            T t = objs[0];
            DataTable dt = new DataTable();
            Dictionary<TableField, PropertyInfo> fields = t.GetRelation();
            foreach (var field in fields)
            {
                dt.Columns.Add(field.Key.Name, field.Value.PropertyType);
            }
            //dt.Columns.Add(t.GetTableKey(), typeof(object));
            dt.PrimaryKey = new DataColumn[] { dt.Columns[t.GetTableKey()] };
            foreach (T obj in objs)
            {
                DataRow dr = dt.NewRow();
                foreach (var item in fields)
                {
                    dr[item.Key.Name] = item.Value.GetValue(obj);
                }
                if (string.IsNullOrEmpty(dr[t.GetTableKey()].ToString()))
                    dr[t.GetTableKey()] = t.GetAutoID();
                dt.Rows.Add(dr);
            }
            using (SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
            {
                bulk.BatchSize = 300;
                bulk.DestinationTableName = t.GetTable();
                foreach (var item in dt.Columns)
                {
                    bulk.ColumnMappings.Add(new SqlBulkCopyColumnMapping(item.ToString(), item.ToString()));
                }
                bulk.WriteToServer(dt);
            }
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
