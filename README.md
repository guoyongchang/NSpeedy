NSpeedy

**是一个轻量级ORM框架**

**可以自己编写复杂的SQL语句进行增删改查**

**暂时仅支持mssql(Sql Server)数据库**

NSpeedy技术QQ群:544863170

**所有接口：**
```C#
Operate:
    //新增 返回受影响行数
    int Insert<T>(T obj) where T : IdentityObj;
    //批量新增 返回受影响行数
    int InsertBatchSame<T>(IList<T> list) where T : IdentityObj;
    //修改 返回受影响行数
    int Update<T>(T obj) where T : IdentityObj;
    //批量修改 返回受影响行数
    int UpdateBatchSame<T>(IList<T> list) where T : IdentityObj;
    //删除 返回受影响行数
    int Delete<T>(T obj) where T : IdentityObj;
    //批量删除 返回受影响行数
    int DeleteBatchSame<T>(IList<T> list) where T : IdentityObj;
    //执行Sql 返回受影响行数
    int ExecuteSql(string sql);
    //批量执行Sql 可返回受影响行数也可返回ID 具体看Sql定义
    int ExecuteMoreSql(IList<string> list);
Query:
    //查询主键一致的指定泛型的对象
    T SelectRelyOnKey<T>(string id) where T : BasicObj;
    //查询指定泛型的对象
    T SelectOne<T>(string sql) where T : BasicObj;
    //查询指定泛型的对象 还是主键
    T SelectOne<T>(T t) where T : IdentityObj;
    //查询一行一列 一般用作查询行数
    T SelectValue<T>(string sql);
    //查询以JSON返回
    string SelectWithJSON(string sql);
    //查询以DataSet返回
    DataSet SelectWithDataSet(string sql, params string[] tableName);
    //查询指定泛型的列表
    IList<T> Select<T>(string sql) where T : BasicObj;
    //查询指定泛型的列表 实际用处不大 只在使用动态程序集时用到
    IList<T> Select<T>(string sql, object obj) where T : BasicObj;
    //查询指定泛型的动态列表
    IList<dynamic> SelectWithDynamic<T>(string sql) where T : BasicObj;
    //查询以动态列表返回
    IList<dynamic> SelectWithDynamic(string sql);
    //查询一列多行
    IList<T> SelectValueList<T>(string sql);
    //查询一行多列
    IDictionary<string, object> SelectOneData(string sql);
Multi: //事物队列
    //给队列中添加一条新增任务
    void AddInsert<T>(T obj) where T : IdentityObj;
    //给队列中添加一条修改任务
    void AddUpdate<T>(T obj) where T : IdentityObj;
    //给队列中添加一条删除任务
    void AddDelete<T>(T obj) where T : IdentityObj;
    //给队列中添加一条执行Sql任务
    void AddExecutesql(string sql);
    //给队列中添加一条批量新增任务
    void AddInsertBatchSame<T>(IList<T> list) where T : IdentityObj;
    //给队列中添加一条批量修改任务
    void AddUpdateBatchSame<T>(IList<T> list) where T : IdentityObj;
    //给队列中添加一条批量删除任务
    void AddDeleteBatchSame<T>(IList<T> obj) where T : IdentityObj;
    //提交队列 返回受影响总行数 顺序执行
    int Commit();
```
***
**如何使用：**
***

```c#
using Example.DataSource;
using Example.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace Example
{
    class Run
    {
        //初始化NSpeedy
        public static DataManager dbm = new DataManager();
        
        static void Main(string[] args)
        {
            Run r = new Run();
            r.InitData();
            Console.ReadKey();
        }
        //初始化一些数据
        public void InitData() {
            //创建一个老师
            TB_Teacher teacher = new TB_Teacher();
            teacher.Name = "李老师";
            teacher.ID = teacher.GetAutoID();//设置一个UUID 方便等会使用
            dbm.Multi.AddInsert(teacher);//给操作队列增加一条记录
            //创建一个班级
            TB_Class class1 = new TB_Class();
            class1.TeacherID = teacher.ID;//刚才给Teacher设置的ID在这里起到作用
            class1.Name = "三年级";
            class1.CreateTime = DateTime.Now.ToString("yyyy-MM-dd");
            dbm.Multi.AddInsert(class1);
            //创建一堆学生
            for (int i = 0; i < 5; i++) {
                dbm.Multi.AddInsert(new TB_Student() {
                    ClassID = 1,//看 这个时候 自增ID的弊端就出现了
                    Name = $"王{i}"
                });
            }
            //再创建一个学生
            TB_Student student = new TB_Student() {
                ClassID = 1,
                ID = "RRRRRRRR",
                Name = "周x"
            };
            dbm.Multi.AddInsert(student);
            //最后提交 这些队列中的东西会存放在一个事物里进行 返回受影响行数
            int line = dbm.Multi.Commit();
        }
        public void Query() {
            //根据主键查一个
            TB_Student s1 = dbm.Query.SelectRelyOnKey<TB_Student>("RRRRRRRR");
            //Result:
            //s1.ID = "RRRRRRRR"
            //s1.Name = "周x"
            //s1.ClassID = 1

            //根据Sql查询一个
            s1 = dbm.Query.SelectOne<TB_Student>(@"
                select * from tb_Student where id = 'RRRRRRRR'
            ");
            //Result:
            //s1.ID = "RRRRRRRR"
            //s1.Name = "周x"
            //s1.ClassID = 1

            //根据sql查一行
            IDictionary<string, object> dics1 = dbm.Query.SelectOneData("select * from tb_Student where id = 'RRRRRRRR'");
            //Result:
            //dics1["ID"] = "RRRRRRRR"
            //dics1["Name"] = "周x"
            //dics1["ClassID"] = 1

            //根据sql查多个
            IList<TB_Student> students = dbm.Query.Select<TB_Student>("select * from tb_Student where name like '王%'");
            //Result:
            //students[0] = Result:
            //              ID = "XXXXXXXXXXXXXXXXXX"//表示不确定
            //              Name = "王1"
            //              ClassID = 1
            //students[1] = Result:
            //              ID = "XXXXXXXXXXXXXXXXXX"//表示不确定
            //              Name = "王2"
            //              ClassID = 1
            //..................

            //查询一行一列
            int allStudents  = dbm.Query.SelectValue<Int32>("select count(ID) from tb_student");
            //Result: 6

            //查询一列多行
            IList<string> studentids = dbm.Query.SelectValueList<string>("select ID from tb_Student");
            //Result:
            //studentids[0]="XXXXXXXXXXXXXXXXX"//表示不确定
            //studentids[1]="XXXXXXXXXXXXXXXXX"//表示不确定
            //studentids[2]="XXXXXXXXXXXXXXXXX"//表示不确定
            //.....................

            //查询多Result 支持多表
            DataSet ds = dbm.Query.SelectWithDataSet("select * from tb_Student;select * from tb_Teacher","StudentTable","TeacherTable");
            //Result:
            //DataTable studentTable = ds.Tables["StudentTable"]
            //DataTable teacherTable = ds.Tables["TeacherTable"]

            //动态查询
            IList<dynamic> dynStudents = dbm.Query.SelectWithDynamic(@"
                select stu.*,class.Name as ClassName from tb_Student stu 
                left join tb_Class class on class.ID = stu.ClassID
            ");
            //Result:
            //dynStudents[0] = Result: 
            //                 ID = "XXXXXXXXXXXXXXXXXX" //表示不确定
            //                 Name = "王1"
            //                 ClassID = 1
            //                 ClassName = "三年级"
            //......................
            //注意 这个将会根据你查询的列名作为动态类型的属性
            //例如 sql为 select stu.id,stu.name,stu.classid,class.Name as classname from tb_Student stu 
            //           left join tb_Class class on class.ID = stu.ClassID 那么结果将会如下
            //Result:
            //dynStudents[0] = Result: 
            //                 id = "XXXXXXXXXXXXXXXXXX" //表示不确定
            //                 name = "王1""
            //                 classid = 1
            //                 classname = "三年级"
            //............................

            //动态查询基于泛型
            IList<dynamic> dynStudents2 = dbm.Query.SelectWithDynamic<TB_Student>($@"
                select stu.id,stu.name,stu.classid,class.Name as classname from tb_Student stu 
                left join tb_Class class on class.ID = stu.ClassID
            ");
            //Result:
            //dynStudents2[0] = Result: 
            //                 ID = "XXXXXXXXXXXXXXXXXX" //表示不确定
            //                 Name = "王1""
            //                 ClassID = 1
            //                 classname = "三年级" //由于该列名"classname"不存在在TB_Student的TableField中 所以无法对应
            //............................

            //查询为json
            string stuJson = dbm.Query.SelectWithJSON("select * from tb_Student");
            //[{"ID":"39175a56-0147-4f0b-bfd2-b3135981af77","Name":"王4","ClassID":1},{"ID":"4fa3bbab-14aa-48bb-bf91-78d55b902c2b","Name":"王0","ClassID":1},{"ID":"9b54d386-e1cf-468d-99b6-5bb9d9bb7ace","Name":"王2","ClassID":1},{"ID":"c3e47fed-4807-41fe-bf44-02f4444f2652","Name":"王3","ClassID":1},{"ID":"de2fe44a-0135-4976-86a7-99f5913c9bb4","Name":"王1","ClassID":1},{"ID":"RRRRRRRR","Name":"周x","ClassID":1}]
            //嗯。。我直接把查询的搬过来了 反正就是这么个样子
        }
        public void Add() {
            //新增一个 立即执行 返回受影响行数
            int ret = dbm.Operate.Insert(new TB_Student() { ClassID=1,Name="增XX" });
            //Result: 1

            //新增一批 立即执行 返回受影响行数
            IList<TB_Student> students = new List<TB_Student>();
            for (int i = 0; i < 5; i++)
            {
                students.Add(new TB_Student()
                {
                    ClassID = 1,
                    Name = $"批增{i}"
                });
            }
            ret = dbm.Operate.InsertBatchSame<TB_Student>(students);
            //Result:5

            //执行Sql 神器ExecuteSql! 立即执行 返回受影响行数
            ret = dbm.Operate.ExecuteSql("insert into tb_Student values(newid(),'神xx',1)");
            //Result:1

            //批量执行Sql 立即执行 返回受影响行数
            IList<string> executeSqls = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                executeSqls.Add($"insert into tb_Student values(newid(),'批神{i}',1)");
            }
            ret = dbm.Operate.ExecuteMoreSql(executeSqls);
            //Result:5
        }
        public void Update() {
            //获取一个Student方便修改
            TB_Student student = dbm.Query.SelectRelyOnKey<TB_Student>("RRRRRRRR");
            student.Name = "改周x";

            //修改 立即执行 返回受影响行数
            //会根据实体类注解[Table]中的Key作为主键来更新其他属性
            int ret = dbm.Operate.Update<TB_Student>(student);
            //Result: 1

            //批量修改 自己结合新增的批量看


            //执行Sql 神器ExecuteSql! 立即执行 返回受影响行数
            ret = dbm.Operate.ExecuteSql($"update tb_Student set Name = '改'+Name where name like '王%'");
            //Result:5
            
        }
    }
}

```

**数据库与C#对象映射关系配置：**

***

```c#
using NSpeedy.Annotate;
using NSpeedy.Object.Basic;

namespace Example.Entity
{
    //Key表示表主键 Name表示表名称
    [Table(Key ="ID",Name ="tb_Class")]
    public class TB_Class:IdentityObj
    {
        /// <summary>
        /// 重写ID属性 由于是自增 Own一定要写成n
        /// </summary>
        [TableField(Name="id",Own ="n")]
        public new string ID { get; set; }
        /// <summary>
        /// 班次名称 TableField中的Name不区分大小写
        /// </summary>
        [TableField(Name = "name", Own = "y")]
        public string Name { get; set; }
        /// <summary>
        /// 班级创建时间
        /// </summary>
        [TableField(Name = "createtime", Own = "y")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 负责教师ID
        /// </summary>
        [TableField(Name = "teacherid", Own = "y")]
        public string TeacherID { get; set; }
    }
}
```
```C#
using NSpeedy.Annotate;
using NSpeedy.Object.Basic;

namespace Example.Entity
{
    //Key表示表主键 Name表示表名称
    [Table(Key="ID",Name ="tb_Student")]
    public class TB_Student:IdentityObj
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        [TableField(Name = "name", Own = "y")]
        public string Name { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        [TableField(Name = "classid", Own = "y")]
        public int ClassID { get; set; }
    }
}
```
```C#
using NSpeedy.Annotate;
using NSpeedy.Object.Basic;

namespace Example.Entity
{
    //Key表示表主键 Name表示表名称
    [Table(Key ="ID",Name ="tb_Teacher")]
    public class TB_Teacher :IdentityObj
    {
        /// <summary>
        /// 教师名称
        /// </summary>
        [TableField(Name = "Name", Own = "y")]
        public string Name { get; set; }
    }
}


```

**数据库连接配置 在`Example.DataSource`中：**

***

```C#
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
        //例:"Data Source=192.168.0.19;Initial Catalog=master;User ID=sa;Password=******;pooling=true;min pool size=5;max pool size=4000;connect timeout =180;";
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

```
