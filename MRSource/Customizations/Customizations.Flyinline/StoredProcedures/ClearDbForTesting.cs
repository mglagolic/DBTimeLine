using Customizations.Flyinline.DBObjects;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customizations.Flyinline.StoredProcedures
{
    public static class ClearDbForTesting
    {
        public static IDBObject Create(IDBSchema sch)
        {
            DBStoredProcedure sp = (DBStoredProcedure)sch.AddDBObject("CleanDataForTesting",
                new DBStoredProcedureDescriptor()
                {
                    Parameters = "",
                    Body = @"
    DELETE FROM Flyinline.UserDetail
	DELETE FROM Common.principalhasrole
	DELETE FROM Common.principal	
",
                    ErrorHandling = true
                },
                new DBRevision(new DateTime(2019, 9, 20), 0, eDBRevisionType.Create));

            return sp;
        }
    }
}
