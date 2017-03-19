using NSpeedy.Query.Behavior;
using NSpeedy.Query.Object.SetValue;

namespace NSpeedy.Query.Object.Single
{
   abstract public class QueryDataObject
    {
        protected DataToObject resultToObject;
        protected QueryData queryData;

        abstract public void Initialize();

        public QueryDataObject()
        {
            Initialize();
        }
    }
}
