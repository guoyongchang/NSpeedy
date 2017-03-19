using NSpeedy.Annotate;
using System;

namespace NSpeedy.Object.Basic
{
    public abstract class IdentityObj : BasicObj
    {
        [TableField(Name = "id", Own = "Y")]
        public string ID { get; set; }
        public string GetAutoID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
