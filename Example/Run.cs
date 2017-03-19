using Example.DataSource;
using Example.Entity;
using NSpeedy.Object.Basic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Run
    {
        public static DataManager dbm = new DataManager();
        static void Main(string[] args)
        {
            //IList<dynamic> list = dbm.Query.SelectWithDynamic("select * from tb_record");
            //(from c in list where Convert.ToDateTime(c.CreateTime ).ToString ("yyyy-MM-dd").Equals("2016-01-01") select c).ToList().ForEach(c => {
            //    list.Remove(c);
            //});
            Run r = new Run();
            r.Add();
            r.Query();
            r.Other();
            Console.ReadKey();
        }
        public void Add() {
            //打断点自己进来看
            int ret1 = dbm.Operate.Insert<TestGID>(new TestGID() { Name=$"GID_{DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss")}", UpdateTime=DateTime.Now.ToString("yyyy-MM-dd")});
            int ret2 = dbm.Operate.Insert<TestAutoID>(new TestAutoID() { Name = $"AutoID_{DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss")}",UpdateTime= DateTime.Now.ToString("yyyy-MM-dd") });
        }
        public void Query(){
            //查询一行多列
            //Key=字段名称 Value=值
            IDictionary<string,object> obj = dbm.Query.SelectOneData("select * from tb_Test_GID");
            
            //主键
            TestGID TG1 = dbm.Query.SelectRelyOnKey<TestGID>("ID");
            //sql 
            TestGID TG2 = dbm.Query.SelectOne<TestGID>("select * from tb_Test_GID");
            //对象 还是找主键
            TestGID TG3 = dbm.Query.SelectOne<TestGID>(new TestGID() { ID="xxx"});

            //查询列表
            IList<TestGID> list1 =dbm.Query.Select<TestGID>("select * from tb_Test_GID");
            //查询一行一列
            Int32 value = dbm.Query.SelectValue<Int32>("select count(id) from tb_Test_GID");
            //一列多行
            IList<Int32> list2 =dbm.Query.SelectValueList<Int32>("select id from tb_Test_AutoID");
            //多表查询 返回DataSet
            DataSet ds = dbm.Query.SelectWithDataSet("select id from tb_Test_AutoID;select id from tb_Test_GID",new string[]{ "AutoTable","GIDTable"});
            DataTable AutoTable = ds.Tables["AutoTable"];
            DataTable GIDTable =  ds.Tables["GIDTable"];
            //直接返回json 可多表
            string JSON = dbm.Query.SelectWithJSON("select id from tb_Test_AutoID;select id from tb_Test_GID");
            //返回值示例 :[{"x":1},{"x":2},{"x":3}]

            //返回动态类型
            IList<dynamic> dyna = dbm.Query.SelectWithDynamic("select * from tb_Test_AutoID");
            foreach (var item in dyna)
            {
                Console.WriteLine($"{item.id}_{item.name}_{item.updatetime}");
            }
            //返回动态类型基于实体结构 一般查询时用
            IList<dynamic> dyna2 = dbm.Query.SelectWithDynamic<TestGID>("select * from tb_Test_AutoID");
            foreach (var item in dyna2)
            {
                Console.WriteLine($"{item.ID}_{item.Name}_{item.UpdateTime}");
            }
        }
        public void Delete() {
            int ret1 = dbm.Operate.Delete<TestGID>(new TestGID() { });
            int ret2 = dbm.Operate.ExecuteSql(@"
                delete tb_Test_GID where id = 'xxx'
            ");
        }
        public void Other() {
            //这是事物部分的代码
            dbm.Multi.AddExecutesql(" update xx set x = 1");
            dbm.Multi.AddInsert(new TestAutoID() { });

            //受影响行数 失败会回滚 只有在提交的时候 才会开启事物模式
            int ret = dbm.Multi.Commit();

            //看源码 我不想写了 我好累
            //还有 
            //ExecuteSql是万能的
            //SelectWithDynamic也是万能的
            //最关键的是Multi
        }
    }
}
