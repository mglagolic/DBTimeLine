

namespace Framework.Persisting.Interfaces
{
    public interface ISqlGenerator
    {
        string GetSql(string sql, string where, string order, int pageNumber = -1, int pageSize = 20);
    }
}
