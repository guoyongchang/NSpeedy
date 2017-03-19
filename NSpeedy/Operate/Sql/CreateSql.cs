using NSpeedy.Object.Basic;

namespace NSpeedy.Operate.Sql
{
    public interface CreateSql
    {
         string Create<T>(T obj) where T : IdentityObj;
    }
}
