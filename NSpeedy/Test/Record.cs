using System;
using System.Data;
using NSpeedy.Annotate;
using NSpeedy.Object.Basic;

namespace NSpeedy.Test
{
    [Table(Key = "ID", Name = "TB_test", Type = "Int32")]
    class Record : IdentityObj, FastSetObj
    {
        [TableField(Name = "PersonID", Own = "y")]
        public int PersonID { get; set; }

        [TableField(Name = "SignOutPhoto", Own = "y")]
        public string SignOutPhoto { get; set; }

        public void SetObject(IDataReader dr)
        {
            this.PersonID = dr.GetInt32(1);
            //this.ID = dr.GetInt32(0);
        }
    }
}
