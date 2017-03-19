using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSpeedy.Annotate
{
    public class Table : Attribute
    {
        public string Name { get; set; } = "";
        public string Key { get; set; } = "";
        public string Type { get; set; } = "";
    }
}
