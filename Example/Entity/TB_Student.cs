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
