using System.Collections.Generic;
using System.Data.Common;

namespace Framework.Persisting.Interfaces
{
    public interface IPersister
    {
        List<IOrderItem> OrderItems { get; }
        string Where { get; set; }
        Dictionary<object, IDlo> GetData(DbTransaction transaction = null);

    }
}
