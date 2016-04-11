using System.Collections.Generic;

namespace Framework.Persisting.Interfaces
{
    public interface IDlo
    {
        // TODO - ovo drukcije napisati, nema smisla na dva mjesta pamtiti iste stvari
        Dictionary<string, object> PrimaryKeyValues { get; }
        Dictionary<string, object> ColumnValues { get; }
    }
}
