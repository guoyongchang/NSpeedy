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
