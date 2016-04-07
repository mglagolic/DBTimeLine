using System;
using Framework.Persisting.Interfaces;
using System.Text;

namespace Framework.Persisting.Implementation
{
    class SqlGenerator : ISqlGenerator
    {
        public string GetSql(string sql, string where, string order, int pageNumber = -1, int pageSize = 20)
        {
            string ret = "";

            var sb = new StringBuilder();
            sb.Append(sql);
            
            if (where != null && where.Length > 0)
            {
                sb.Append("\nWHERE\n\t");
                sb.Append(where);
            }
            if (order != null && order.Length > 0)
            {
                sb.Append("\nORDER BY\n\t");
                sb.Append(order);
            }
            if (pageNumber != -1)
            {
                sb.Append("\n\tOFFSET ");
                sb.Append(((pageNumber - 1) * pageSize).ToString());
                sb.Append(" ROWS FETCH NEXT ");
                sb.Append(pageSize.ToString());
                sb.Append(" ROWS ONLY");
            }
            ret = sb.ToString();

            return ret;
        }
    }
}
