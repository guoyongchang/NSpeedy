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
