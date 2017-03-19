using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DataSource
{
    public class DataManagerCreater
    {
        private static DataManagerCreater singleton;
        private NSpeedy.Manager.AbstractDataOperater _dbm;
        private static readonly object syncObject = new object();
        private DataManagerCreater()
        {
            _dbm = new DataManager();
        }
        public NSpeedy.Manager.AbstractDataOperater GetDBM() { return _dbm; }
        public static DataManagerCreater GetInstance()
        {
            if (singleton == null)
            {
                lock (syncObject)
                {
                    if (singleton == null)
                    {
                        singleton = new DataManagerCreater();
                    }
                }
            }
            return singleton;
        }
    }
}
