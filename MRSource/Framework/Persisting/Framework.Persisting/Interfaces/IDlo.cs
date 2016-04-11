using System.Collections.Generic;

namespace Framework.Persisting.Interfaces
{
    public interface IDlo
    {
        Dictionary<string, object> ColumnValues { get; }
    }
}
