using System.Collections.Generic;
using System.Data.Common;

namespace Framework.Persisting.Interfaces
{
    // TODO - obrisati ovo kad sve bude gotovo, nema smisla interface, jer koristi propertie specificne za sql db
    // CONSIDER - razmisliti o seljenju persister klase u implementation i izradi ipersister tako da podrzava razlicita mjesta pohrane
    public interface IPersister
    {
        DbConnection CNN { get; set; }

        string Where { get; set; }
        List<IOrderItem> OrderItems { get; }
        int PageSize { get; set; }
        
        //HashSet<IDlo> GetDataHashSet(DbTransaction transaction, int pageNumber = -1);
        List<IDlo> GetData(DbTransaction transaction, int pageNumber = -1);
    }


}
