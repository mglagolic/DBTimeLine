

namespace Framework.Persisting.Interfaces
{
    public interface ISqlGenerator
    {
        string GetSql(string sql, string where, string order);
        string GetSqlPage(int pageNumber);

    }
}
