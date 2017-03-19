using Example.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Run
    {
        static void Main(string[] args)
        {
            var dbm = new DataManager();
            var List = dbm.Query.SelectWithDynamic("select * from tb_Si_EmpfeeTemplete");
            Console.ReadKey();
        }
    }
}
