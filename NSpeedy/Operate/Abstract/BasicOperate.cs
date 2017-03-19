using NSpeedy.Operate.Sql;

namespace NSpeedy.Operate.Abstract
{
    public class BasicOperate
    {
        private CreateSql createSql;

        public BasicOperate(CreateSql createSql)
        {
            this.createSql = createSql;
        }

        public CreateSql GetCreateSql()
        {
            return createSql;
        }
    }
}
