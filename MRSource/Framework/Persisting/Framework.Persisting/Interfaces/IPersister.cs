using System.Collections.Generic;
using System.Data.Common;

namespace Framework.Persisting.Interfaces
{
    public interface IPersister
    {
        List<IOrderItem> OrderItems { get; }
        string Where { get; set; }
        HashSet<IDlo> GetData(DbTransaction transaction = null);

    }
}
