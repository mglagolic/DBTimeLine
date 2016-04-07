using System;
using Framework.Persisting.Interfaces;

namespace Framework.Persisting.Implementation
{
    class SqlGenerator : ISqlGenerator
    {
        public string GetSql(string sql, string where, string order)
        {
            string ret = "";


            return ret;
        }

        public string GetSqlPage(int pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
