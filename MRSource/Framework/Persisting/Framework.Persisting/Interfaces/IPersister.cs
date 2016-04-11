using System.Collections.Generic;
using System.Data.Common;

namespace Framework.Persisting.Interfaces
{
    public interface IPersister
    {
        DbConnection CNN { get; set; }

        string Sql { get; }
        string Where { get; set; }
        List<IOrderItem> OrderItems { get; }
        int PageSize { get; set; }
        
        HashSet<IDlo> GetData(DbTransaction transaction, int pageNumber = -1);
    }


}
