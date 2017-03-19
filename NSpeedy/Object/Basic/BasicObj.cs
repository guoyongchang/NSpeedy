using NSpeedy.Annotate;
using NSpeedy.Object.Basic.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data;

namespace NSpeedy.Object.Basic
{
    public abstract class BasicObj : TableRelation,ICloneable
    {
        public Dictionary<TableField, PropertyInfo> GetRelation()
        {
            Type type = this.GetType();
            Dictionary<TableField, PropertyInfo> Relation = new Dictionary<TableField, PropertyInfo>();
            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                object[] objAttrs = propInfo.GetCustomAttributes(typeof(TableField), true);
                if (objAttrs.Length > 0)
                {
                    TableField attr = objAttrs[0] as TableField;
                    if (attr != null)
                    {
                        Relation.Add(attr, propInfo);
                    }
                }
            }
            //Key：注解，VALUE：字段名称
            return (from c in Relation where c.Key.Own.ToUpper().Equals("Y") select c).ToDictionary(k => k.Key, v => v.Value);
            //return Relation;
        }
        public Dictionary<TableField, PropertyInfo> GetRelation(List<string> fields)
        {
            Type type = this.GetType();
            Dictionary<TableField, PropertyInfo> Relation = new Dictionary<TableField, PropertyInfo>();
            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                object[] objAttrs = propInfo.GetCustomAttributes(typeof(TableField), true);
                if (objAttrs.Length > 0)
                {
                    TableField attr = objAttrs[0] as TableField;
                    if (attr != null)
                    {
                        Relation.Add(attr, propInfo);
                    }
                }
            }
            //Key：注解，VALUE：字段名称

            Dictionary<TableField, PropertyInfo> newRelation = new Dictionary<TableField, PropertyInfo>();
            foreach (var item in fields)
            {
                var Entity = (from c in Relation where c.Key.Name.ToLower().Equals(item.ToLower()) select c).FirstOrDefault();
                if (Entity.Key != null)
                {
                    newRelation.Add(Entity.Key, Entity.Value);
                }
            }
            return newRelation;
        }

        public string GetTable()
        {
            Annotate.Table attr = this.GetType().GetCustomAttributes(typeof(Annotate.Table), true)[0] as Annotate.Table;
            return attr.Name;
        }

        public string GetTableKey()
        {
            Annotate.Table attr = this.GetType().GetCustomAttributes(typeof(Annotate.Table), true)[0] as Annotate.Table;
            return attr.Key;
        }

        public string GetTableKeyType()
        {
            Annotate.Table attr = this.GetType().GetCustomAttributes(typeof(Annotate.Table), true)[0] as Annotate.Table;
            return attr.Type;
        }

        public IList<PropertyInfo> GetFields()
        {
            Type type = this.GetType();
            System.Reflection.PropertyInfo[] fields = type.GetProperties();
            return fields.ToList();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
