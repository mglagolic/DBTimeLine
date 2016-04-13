using Framework.Persisting.Interfaces;
using System.Text;
using System.Collections.Generic;
using Framework.Persisting.Enums;

namespace Framework.Persisting.Implementation
{
    class SqlGenerator : ISqlGenerator
    {
        public string GetOrderByClause(List<IOrderItem> OrderItems)
        {
            var sb = new StringBuilder();
            foreach (var oi in OrderItems)
            {
                sb.Append(oi.SqlName + " ");
                if (oi.Direction == eOrderDirection.Descending)
                {
                    sb.Append("DESC ");
                }
                sb.Append(",");

            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString().Trim();
        }

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
