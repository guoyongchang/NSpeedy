using NSpeedy.Annotate;
using NSpeedy.Object.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Entity
{
    //key是主键的意思 主要作用在删改查 Name则表示表名
    [Table(Key ="id",Name ="tb_Test_GID")]
    public class TestGID : IdentityObj
    {
        //Own为y时 在进行增删改的时候会检测到该字段 为n时 代表该字段为查询字段
        //Name不区分大小写 例如 select NAmE from tb_Test_GID 一样可以将值设置到字段里来
        [TableField(Name ="name",Own ="y")]
        public string Name { get; set; }
        
        [TableField(Name ="updatetime",Own ="y")]
        public string UpdateTime { get; set; }

        //不加TableField表示这个属性与NSpeedy无任何关联
        public string Output { get { return $"{this.Name}_{this.UpdateTime}"; } }
    }
}
