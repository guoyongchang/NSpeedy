using NSpeedy.Manager.Basic;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NSpeedy.Manager
{
    public abstract class AbstractDataOperater : ConnectionManager
    {
        public IDictionary<string, object> Dic { get; }
        public MultipleOperation Multi { get; } 
        public SingleOperation Operate { get { return new SingleOperater(this); } }
        public QueryOperation Query { get { return new QueryOperater(this); } }
        public AbstractDataOperater()
        {
            Dic = new Dictionary<string, object>();
            Multi = new MultipleOperater(this,Dic);
            //Operate = new SingleOperater(this);
            //Query = new QueryOperater(this);
        }
        public abstract SqlConnection GetConnection();

        public abstract void CloseConn(SqlConnection conn);
    }
}
